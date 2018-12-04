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
using Dolittle.Logging;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Represents a system that provides the bindings for the Resource system
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly ITypeFinder _typeFinder;
        readonly ILogger _logger;
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> used for discovering types by the resource system</param>
        /// <param name="container"><see cref="IContainer"/> to use for getting instances</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Bindings(ITypeFinder typeFinder, IContainer container, ILogger logger)
        {
            _typeFinder = typeFinder;
            _logger = logger;
            _container = container;
        }


        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<ICanProvideResourceConfigurationsByTenant>().To<ResourceConfigurationsByTenantProvider>();
            var resourceConfiguration = new ResourceConfiguration(_typeFinder, _container, _logger);
            builder.Bind<IResourceConfiguration>().To(resourceConfiguration);

            var resourceTypeTypes = _typeFinder.FindMultiple<IAmAResourceType>();

            var resourceTypeServices = resourceTypeTypes.Select(_ => _container.Get(_) as IAmAResourceType).SelectMany(_ => _.Services);
            resourceTypeServices.ForEach(_ => builder.Bind(_).To(() => resourceConfiguration.GetImplementationFor(_)));
        }
    }
}