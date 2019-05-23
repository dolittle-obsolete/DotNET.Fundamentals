/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;

namespace Dolittle.DependencyInversion.Autofac
{


    /// <summary>
    /// Represents a <see cref="IRegistrationSource"/> that deals with resolving open generic type callbacks
    /// </summary>
    public class OpenGenericTypeCallbackRegistrationSource : IRegistrationSource
    {
        static IDictionary<Type, Func<Type>> _typeCallbackByService = new Dictionary<Type, Func<Type>>();
        internal static void AddService(KeyValuePair<Type, Func<Type>> typeCallbackAndServicePair)
        {
             _typeCallbackByService.Add(typeCallbackAndServicePair);
        }

        /// <inheritdoc/>
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc/>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var serviceWithType = service as IServiceWithType;
            
            if (serviceWithType == null ||
                !serviceWithType.ServiceType.IsGenericType ||
                !_typeCallbackByService.ContainsKey(serviceWithType.ServiceType.GetGenericTypeDefinition()))
            {
                return Enumerable.Empty<IComponentRegistration>();
            }
            var serviceOpenGenericType = serviceWithType.ServiceType.GetGenericTypeDefinition();
            var callback = _typeCallbackByService[serviceOpenGenericType];
            
            var registration = new ComponentRegistration(
                Guid.NewGuid(),
                new DelegateActivator(serviceWithType.ServiceType, (c, p) => c.ResolveUnregistered(callback().MakeGenericType(serviceWithType.ServiceType.GetGenericArguments()))),
                new CurrentScopeLifetime(),
                InstanceSharing.None,
                InstanceOwnership.OwnedByLifetimeScope,
                new[] { service },
                new Dictionary<string, object>()
            );
            
            return new[] { registration };
        }
    }
}