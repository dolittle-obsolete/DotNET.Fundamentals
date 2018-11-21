/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Immutability;
using Dolittle.Types;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents the system that manages the bindings for the IoC container for the 
    /// <see cref="IConfigurationObject">configuration objects</see> in the system
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/></param>
        /// <param name="container"><see cref="IContainer"/></param>
        public Bindings(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var configurationObjectProviders = new ConfigurationObjectProviders(_typeFinder, _container);
            builder.Bind<IConfigurationObjectProviders>().To(configurationObjectProviders);

            var configurationObjectTypes = _typeFinder.FindMultiple<IConfigurationObject>();
            configurationObjectTypes.ForEach(_ => 
            {
                _.ShouldBeImmutable();
                builder.Bind(_).To(() => configurationObjectProviders.Provide(_));
            });
        }
    }
}