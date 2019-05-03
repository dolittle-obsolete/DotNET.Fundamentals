/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Logging;
using Dolittle.Scheduling;

namespace Dolittle.Types.Bootstrap
{
    /// <summary>
    /// Represents the entrypoint for starting up and initialization for an app using the Type system
    /// </summary>
    public class Boot
    {
        /// <summary>
        /// Initialize systems needed for the type system and discovery mechanisms to work
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> that will be used</param>
        /// <param name="scheduler"><see cref="IScheduler"/> to use for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="entryAssembly"><see cref="Assembly"/> to use as entry assembly - null indicates it will ask for the entry assembly</param>
        /// <returns><see cref="ITypeFinder"/> that can be used</returns>
        public static ITypeFinder Start(IAssemblies assemblies, IScheduler scheduler, ILogger logger, Assembly entryAssembly = null)
        {
            if (entryAssembly == null) entryAssembly = Assembly.GetEntryAssembly();

            IContractToImplementorsMap contractToImplementorsMap;
            if (CachedContractToImplementorsMap.HasCachedMap(entryAssembly))
            {
                logger.Information("Contract to implementors map cache found - using it instead of dynamically discovery");
                contractToImplementorsMap = new CachedContractToImplementorsMap(new ContractToImplementorsSerializer(logger), entryAssembly);
            }
            else
            {
                logger.Information("Using runtime discovery for contract to implementors map");
                contractToImplementorsMap = new ContractToImplementorsMap(scheduler);
                contractToImplementorsMap.Feed(entryAssembly.GetTypes());

                var typeFeeder = new TypeFeeder(scheduler, logger);
                typeFeeder.Feed(assemblies, contractToImplementorsMap);
            }

            var typeFinder = new TypeFinder(contractToImplementorsMap);
            return typeFinder;
        }
    }
}