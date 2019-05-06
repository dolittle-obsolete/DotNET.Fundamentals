/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Logging;
using Dolittle.Scheduling;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeFinder"/>
    /// </summary>
    public class TypeFeeder : ITypeFeeder
    {
        readonly IScheduler _scheduler;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeFeeder"/>
        /// </summary>
        /// <param name="scheduler"><see cref="IScheduler"/> to use for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        public TypeFeeder(IScheduler scheduler, ILogger logger)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Feed(IAssemblies assemblies, IContractToImplementorsMap map)
        {
            _scheduler.PerformForEach(assemblies.GetAll(), assembly => 
            {
                try
                {
                    var types = assembly.GetTypes();
                    map.Feed(types);
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        _logger.Error($"Failed to load: {loaderException.Source} {loaderException.Message}");
                }
            });
        }
    }
}
