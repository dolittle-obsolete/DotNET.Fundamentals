/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies;
using Dolittle.Booting;
using Dolittle.IO;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.PrepareBoot"/> stage of booting
    /// </summary>
    public class Container : ICanPerformBootStage<ContainerSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Container;

        /// <inheritdoc/>
        public void Perform(ContainerSettings settings, IBootStageBuilder builder)
        {
            IBindingCollection resultingBindings;
            var logger = builder.GetAssociation(WellKnownAssociations.Logger) as ILogger;
            var typeFinder = builder.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder;
            var scheduler = builder.GetAssociation(WellKnownAssociations.Scheduler) as IScheduler;
            
            var bindings = builder.GetAssociation(WellKnownAssociations.Bindings) as IBindingCollection;
            var assemblies = builder.GetAssociation(WellKnownAssociations.Assemblies) as IAssemblies;

            var fileSystem = new FileSystem();

            if( settings.ContainerType != null ) 
            {
                logger.Trace($"Starting DependencyInversion with predefined container type '{settings.ContainerType.AssemblyQualifiedName}'");
                resultingBindings = Dolittle.DependencyInversion.Booting.Boot.Start(
                    assemblies,
                    typeFinder,
                    scheduler,
                    fileSystem,
                    logger,
                    settings.ContainerType,
                    bindings,
                    builder.Container as BootContainer);
            } 
            else 
            {
                var bootResult = Dolittle.DependencyInversion.Booting.Boot.Start(
                    assemblies,
                    typeFinder,
                    scheduler,
                    fileSystem,
                    logger,
                    bindings,
                    builder.Container as BootContainer);
                resultingBindings = bootResult.Bindings;
                builder.UseContainer(bootResult.Container);
                logger.Trace($"Using container of type '{builder.Container.GetType().AssemblyQualifiedName}'");
            }

            builder.Associate(WellKnownAssociations.Bindings, resultingBindings);
        }
    }
}