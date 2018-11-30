/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Immutability;
using Dolittle.Logging;
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
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/></param>
        /// <param name="container"><see cref="IContainer"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Bindings(
            ITypeFinder typeFinder,
            IContainer container,
            ILogger logger)
        {
            _typeFinder = typeFinder;
            _container = container;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var configurationObjectProviders = new ConfigurationObjectProviders(_typeFinder, _container, _logger);
            builder.Bind<IConfigurationObjectProviders>().To(configurationObjectProviders);

            var configurationObjectTypes = _typeFinder.FindMultiple<IConfigurationObject>();
            configurationObjectTypes.ForEach((System.Action<System.Type>)(_ => 
            {
                _logger.Trace((string)$"Bind configuration object '{_.GetFriendlyConfigurationName()} - {_.AssemblyQualifiedName}'");
                _.ShouldBeImmutable();
                builder.Bind(_).To((System.Func<object>)(() => {
                    var instance = configurationObjectProviders.Provide(_);
                    _logger.Trace((string)$"Providing configuration object '{_.GetFriendlyConfigurationName()} - {_.AssemblyQualifiedName}' - {instance.GetHashCode()}");
                    return instance;
                }));
            }));
        }
    }
}