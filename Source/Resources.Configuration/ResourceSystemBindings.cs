/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;
using Dolittle.Types;
using Dolittle.Collections;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Represents a system that provides the bindings for the Resource system
    /// </summary>
    public class ResourceSystemBindings : ICanProvideBindings
    {
        internal static ITypeFinder TypeFinder;

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanProvideResourceConfigurationsByTenant>().To<DolittleResourceConfigurationsByTenantProvider>();
            var resourceConfiguration = new ResourceConfiguration(TypeFinder);
            builder.Bind<IResourceConfiguration>().To(resourceConfiguration);

            var resourceTypeTypes = TypeFinder.FindMultiple<IAmAResourceType>();
            resourceTypeTypes.ForEach(_ => ThrowIfNoDefaultConstructor(_));

            var resourceTypeServices = resourceTypeTypes.Select(_ => Activator.CreateInstance(_) as IAmAResourceType).SelectMany(_ => _.Services);
            resourceTypeServices.ForEach(_ => builder.Bind(_).To(() => resourceConfiguration.GetImplementationFor(_)));
        }
        void ThrowIfNoDefaultConstructor(Type resourceTypeType)
        {
            if (! resourceTypeType.HasDefaultConstructor()) throw new InvalidResourceTypeFound($"The ResourceType representation {resourceTypeType.FullName} must have default constructor.");
        }
    }
}