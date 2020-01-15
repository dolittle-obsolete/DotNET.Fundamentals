// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Assemblies.Rules;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.Discovery"/> stage of booting.
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

            logger.Information("  Discover all assemblies");
            var assemblies = Assemblies.Bootstrap.Boot.Start(logger, entryAssembly, settings.AssemblyProvider, _ =>
            {
                if (settings.IncludeAssembliesStartWith?.Count() > 0)
                {
                    settings.IncludeAssembliesStartWith.ForEach(name => logger.Information($"Including assemblies starting with '{name}'"));
                    _.ExceptAssembliesStartingWith(settings.IncludeAssembliesStartWith.ToArray());
                }
            });
            logger.Information("  Set up type system for discovery");
            var typeFinder = Types.Bootstrap.Boot.Start(assemblies, scheduler, logger, entryAssembly);
            logger.Information("  Type system ready");

            builder.Bindings.Bind<IAssemblies>().To(assemblies);
            builder.Bindings.Bind<ITypeFinder>().To(typeFinder);

            builder.Associate(WellKnownAssociations.Assemblies, assemblies);
            builder.Associate(WellKnownAssociations.TypeFinder, typeFinder);
        }
    }
}