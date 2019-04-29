/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Dolittle.Lifecycle;
using Dolittle.Reflection;

namespace Dolittle.DependencyInversion.Autofac.Tenancy
{
    /// <summary>
    /// Represents a <see cref="IRegistrationSource"/> that deals with 
    /// </summary>
    public class BindingsPerTenantsRegistrationSource : IRegistrationSource
    {
        static List<Binding> _bindings = new List<Binding>();
        readonly InstancesPerTenant _instancesPerTenant;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingsPerTenantsRegistrationSource"/>
        /// </summary>
        /// <param name="instancesPerTenant"></param>
        public BindingsPerTenantsRegistrationSource(InstancesPerTenant instancesPerTenant)
        {
            _instancesPerTenant = instancesPerTenant;
        }

        /// <inheritdoc/>
        public bool IsAdapterForIndividualComponents => false;

        /// <summary>
        /// Add a <see cref="Binding"/> for the registration source to use
        /// </summary>
        /// <param name="binding"><see cref="Binding"/> to add</param>
        public static void AddBinding(Binding binding)
        {
            _bindings.Add(binding);
        }

        /// <inheritdoc/>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var serviceWithType = service as IServiceWithType;
            if( serviceWithType == null ) return Enumerable.Empty<IComponentRegistration>();

            if (serviceWithType.ServiceType.HasAttribute<SingletonPerTenantAttribute>() &&
                (!HasService(serviceWithType.ServiceType) &&
                !IsGenericAndHasGenericService(serviceWithType.ServiceType)))
            {
                AddBinding(new Binding(serviceWithType.ServiceType, new Strategies.Type(serviceWithType.ServiceType), new Scopes.Transient()));
            }

            if( serviceWithType == null ||
                (!HasService(serviceWithType.ServiceType) &&
                !IsGenericAndHasGenericService(serviceWithType.ServiceType)))
                return Enumerable.Empty<IComponentRegistration>();

            var registration = new ComponentRegistration(
                Guid.NewGuid(),

                new DelegateActivator(serviceWithType.ServiceType, 
                    (c, p) => 
                        _instancesPerTenant.Resolve(c, GetBindingFor(serviceWithType.ServiceType), serviceWithType.ServiceType)),

                new CurrentScopeLifetime(),
                InstanceSharing.None,
                InstanceOwnership.OwnedByLifetimeScope,
                new[] { service },
                new Dictionary<string, object>()
            );
            
            return new[] { registration };
        }

        bool HasService(Type service)
        {
            return _bindings.Any(_ => _.Service == service);
        }
        
        bool IsGenericAndHasGenericService(Type service)
        {
            return service.IsGenericType && _bindings.Any(_ => _.Service == service.GetGenericTypeDefinition());
        }

        Binding GetBindingFor(Type service)
        {
            var binding = _bindings.SingleOrDefault(_ => _.Service == service);
            if( binding == null && service.IsGenericType) binding = _bindings.Single(_ => _.Service == service.GetGenericTypeDefinition());
            if( binding == null ) throw new ArgumentException($"Couldn't find a binding for service {service.AssemblyQualifiedName}");
            return binding;
        }
   }
}