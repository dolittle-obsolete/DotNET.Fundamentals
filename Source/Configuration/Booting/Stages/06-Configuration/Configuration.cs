/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;
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
    public class Configuration : ICanPerformBootStage<NoSettings>
    {
         /// <inheritdoc/>
        public BootStage BootStage => BootStage.Configuration;

         /// <inheritdoc/>
        public void Perform(NoSettings settings, IBootStageBuilder builder)
        {
            var typeFinder = builder.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder;
            var logger = builder.GetAssociation(WellKnownAssociations.Logger) as ILogger;
            
            var configurationObjectProviders = new ConfigurationObjectProviders(typeFinder, builder.Container, logger);
            builder.Binding.Bind<IConfigurationObjectProviders>().To(configurationObjectProviders);

            var configurationObjectTypes = typeFinder.FindMultiple<IConfigurationObject>();
            configurationObjectTypes.ForEach((System.Action<System.Type>)(_ => 
            {
                logger.Trace((string)$"Bind configuration object '{_.GetFriendlyConfigurationName()} - {_.AssemblyQualifiedName}'");
                _.ShouldBeImmutable();
                builder.Bindings.Bind(_).To((System.Func<object>)(() => {
                    var instance = configurationObjectProviders.Provide(_);
                    logger.Trace((string)$"Providing configuration object '{_.GetFriendlyConfigurationName()} - {_.AssemblyQualifiedName}' - {instance.GetHashCode()}");
                    return instance;
                }));
            }));
        }
    }   
}