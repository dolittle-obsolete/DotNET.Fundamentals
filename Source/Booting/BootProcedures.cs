// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents the main bootstrapper that enables systems to be called during booting of the system.
    /// </summary>
    public class BootProcedures : IBootProcedures
    {
        /// <summary>
        /// Gets the <see cref="CorrelationId"/> used by the <see cref="BootProcedures"/>.
        /// </summary>
        public static readonly CorrelationId BootProceduresCorrelationId = Guid.Parse("85c1a3c9-7d70-4e65-8996-914fa4bc8300");

        readonly IInstancesOf<ICanPerformBootProcedure> _procedures;
        readonly ILogger _logger;
        readonly IExecutionContextManager _executionContextManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedures"/> class.
        /// </summary>
        /// <param name="procedures"><see cref="IInstancesOf{T}"/> of <see cref="ICanPerformBootProcedure"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for working with the <see cref="ExecutionContext"/>.</param>
        public BootProcedures(
            IInstancesOf<ICanPerformBootProcedure> procedures,
            ILogger logger,
            IExecutionContextManager executionContextManager)
        {
            _procedures = procedures;
            _logger = logger;
            _executionContextManager = executionContextManager;
        }

        /// <inheritdoc/>
        public void Perform()
        {
            _logger.Trace("Bootstrapper start all procedures");
            _executionContextManager.System(BootProceduresCorrelationId);
            var queue = new Queue<ICanPerformBootProcedure>(_procedures);

            _logger.Information($"Starting to perform {queue.Count} boot procedures");
            while (queue.Count > 0)
            {
                var procedure = queue.Dequeue();
                if (procedure.CanPerform())
                {
                    _logger.Information($"Performing boot procedure called '{procedure.GetType().AssemblyQualifiedName}'");
                    procedure.Perform();
                }
                else
                {
                    _logger.Information($"Re-enqueing boot procedure called '{procedure.GetType().AssemblyQualifiedName}'");
                    queue.Enqueue(procedure);
                }
            }
        }
    }
}