/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;

namespace Dolittle.DependencyInversion.Autofac
{
    /// <summary>
    /// Represents a <see cref="IRegistrationSource"/> that deals with resolving <see cref="FactoryFor{T}"/>
    /// </summary>
    public class FactoryForRegistrationSource : IRegistrationSource
    {
        internal class FactoryForClass<T>
        {
            public static IContainer container = null;

            public static TType Activate<TType>()
            {
                return container.Get<TType>();
            }
        }

        /// <inheritdoc/>
        public bool IsAdapterForIndividualComponents => false;

        /// <inheritdoc/>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var serviceWithType = service as IServiceWithType;
            if (serviceWithType == null ||
                !serviceWithType.ServiceType.IsGenericType ||
                serviceWithType.ServiceType != typeof(FactoryFor<>).MakeGenericType(serviceWithType.ServiceType.GetGenericArguments() [0]))
            {
                return Enumerable.Empty<IComponentRegistration>();
            }

            var registration = new ComponentRegistration(
                Guid.NewGuid(),
                new DelegateActivator(serviceWithType.ServiceType, (c, p) => {
                    var container = c.Resolve<IContainer>();
                    var typeForFactory = serviceWithType.ServiceType.GetGenericArguments() [0];
                    var wrapperType = typeof(FactoryForClass<>).MakeGenericType(typeForFactory);
                    var containerField = wrapperType.GetField("container");
                    containerField.SetValue(null, container);
                    var activateMethod = wrapperType.GetMethod("Activate").MakeGenericMethod(typeForFactory);
                    var factoryDelegate = activateMethod.CreateDelegate(serviceWithType.ServiceType);
                    return factoryDelegate;
                }),
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