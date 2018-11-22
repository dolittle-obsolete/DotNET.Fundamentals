/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the main bootstrapper that enables systems to be called during booting of the system
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Gets the <see cref="CorrelationId"/> used by the <see cref="Bootstrapper"/>
        /// </summary>
        public static readonly CorrelationId BootstrapperCorrelationId = Guid.Parse("85c1a3c9-7d70-4e65-8996-914fa4bc8300");

        /// <summary>
        /// Start the boot
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances</param>
        public static void Start(ILogger logger, IContainer container)
        {
            logger.Information("Bootstrapper start all procedures");
            var procedures = container.Get<IInstancesOf<ICanPerformBootProcedure>>();
            var queue = new Queue<ICanPerformBootProcedure>(procedures);
            var executionContextManager = container.Get<IExecutionContextManager>();
            executionContextManager.System(BootstrapperCorrelationId);

            logger.Information($"Starting to perform {queue.Count} boot procedures");
            while (queue.Count > 0)
            {
                var procedure = queue.Dequeue();
                if (procedure.CanPerform())
                {
                    logger.Information($"Performing boot procedure called '{procedure.GetType().AssemblyQualifiedName}'");
                    procedure.Perform();
                }
                else
                {
                    logger.Information($"Re-enqueing boot procedure called '{procedure.GetType().AssemblyQualifiedName}'");
                    queue.Enqueue(procedure);
                }
            }
        }
    }
}