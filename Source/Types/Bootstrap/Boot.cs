// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Logging;
using Dolittle.Scheduling;

namespace Dolittle.Types.Bootstrap
{
    /// <summary>
    /// Represents the entrypoint for starting up and initialization for an app using the Type system.
    /// </summary>
    public static class Boot
    {
        /// <summary>
        /// Initialize systems needed for the type system and discovery mechanisms to work.
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> that will be used.</param>
        /// <param name="scheduler"><see cref="IScheduler"/> to use for scheduling work.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        /// <param name="entryAssembly"><see cref="Assembly"/> to use as entry assembly - null indicates it will ask for the entry assembly.</param>
        /// <returns><see cref="ITypeFinder"/> that can be used.</returns>
        public static ITypeFinder Start(IAssemblies assemblies, IScheduler scheduler, ILogger logger, Assembly entryAssembly = null)
        {
            if (entryAssembly == null) entryAssembly = Assembly.GetEntryAssembly();

            IContractToImplementorsMap contractToImplementorsMap;

#if false   // Re-enable when https://github.com/dolittle-fundamentals/DotNET.Fundamentals/issues/219 is fixed
            if (CachedContractToImplementorsMap.HasCachedMap(entryAssembly))
            {
                var before = DateTime.UtcNow;
                logger.Information("Contract to implementors map cache found - using it instead of dynamically discovery");
                contractToImplementorsMap = new CachedContractToImplementorsMap(
                    new ContractToImplementorsSerializer(logger), 
                    entryAssembly);
                Console.WriteLine($"CachedMap : {DateTime.UtcNow.Subtract(before).ToString("G")}");
            }
            else
#endif
            {
                logger.Information("Using runtime discovery for contract to implementors map");
                contractToImplementorsMap = new ContractToImplementorsMap(scheduler);
                contractToImplementorsMap.Feed(entryAssembly.GetTypes());

                var typeFeeder = new TypeFeeder(scheduler, logger);
                typeFeeder.Feed(assemblies, contractToImplementorsMap);
            }

            return new TypeFinder(contractToImplementorsMap);
        }
    }
}