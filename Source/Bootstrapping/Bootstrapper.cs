/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
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
        /// Start the boot
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances</param>
        public static void Start(IContainer container)
        {
            var procedures = container.Get<IInstancesOf<ICanPerformBootProcedure>>();
            var logger = container.Get<ILogger>();
            var queue = new Queue<ICanPerformBootProcedure>(procedures);

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