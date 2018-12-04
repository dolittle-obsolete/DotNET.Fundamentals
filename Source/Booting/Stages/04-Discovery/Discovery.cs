/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.Discovery"/> stage of booting
    /// </summary>
    public class Discovery : ICanPerformBootStage<DiscoverySettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Discovery;

        /// <inheritdoc/>
        public void Perform(DiscoverySettings settings, IBootStageBuilder builder)
        {
            var entryAssembly = builder.GetAssociation(WellKnownAssociations.EntryAssembly) as Assembly;
            var logger = builder.GetAssociation(WellKnownAssociations.Logger) as ILogger;
            var scheduler = builder.GetAssociation(WellKnownAssociations.Scheduler) as IScheduler;

            var assemblies = Dolittle.Assemblies.Bootstrap.Boot.Start(logger, entryAssembly, settings.AssemblyProvider);
            var typeFinder = Dolittle.Types.Bootstrap.Boot.Start(assemblies, scheduler, logger, entryAssembly);

            builder.Bindings.Bind<IAssemblies>().To(assemblies);
            builder.Bindings.Bind<ITypeFinder>().To(typeFinder);

            builder.Associate(WellKnownAssociations.Assemblies, assemblies);
            builder.Associate(WellKnownAssociations.TypeFinder, typeFinder);
        }
    }
}