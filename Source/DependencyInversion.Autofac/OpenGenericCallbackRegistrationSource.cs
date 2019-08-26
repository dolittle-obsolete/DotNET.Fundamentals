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
    public class OpenGenericCallbackRegistrationSource : IRegistrationSource
    {
        static readonly IDictionary<Type, Func<IServiceWithType, object>> _callbackByService = new Dictionary<Type, Func<IServiceWithType, object>>();
        internal static void AddService(KeyValuePair<Type, Func<IServiceWithType, object>> typeCallbackAndServicePair)
        {
             _callbackByService.Add(typeCallbackAndServicePair);
        }

        /// <inheritdoc/>
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc/>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var serviceWithType = service as IServiceWithType;
            
            if (serviceWithType == null ||
                !serviceWithType.ServiceType.IsGenericType ||
                !_callbackByService.ContainsKey(serviceWithType.ServiceType.GetGenericTypeDefinition()))
            {
                return Enumerable.Empty<IComponentRegistration>();
            }

            var serviceOpenGenericType = serviceWithType.ServiceType.GetGenericTypeDefinition();
            var callback = _callbackByService[serviceOpenGenericType];
            
            var registration = new ComponentRegistration(
                Guid.NewGuid(),
                new DelegateActivator(serviceWithType.ServiceType, (c, p) => callback(serviceWithType)),
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