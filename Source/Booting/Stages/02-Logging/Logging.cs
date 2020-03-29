// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Execution;
using Dolittle.Logging;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.Logging"/> stage of booting.
    /// </summary>
    public class Logging : ICanPerformBootStage<LoggingSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Logging;

        /// <inheritdoc/>
        public void Perform(LoggingSettings settings, IBootStageBuilder builder)
        {
            var loggerManager = settings.DisableLogging ? NullLoggerManager.Instance : LoggerManager.Instance;

            if (settings.LogMessageWriterCreators != null)
            {
                loggerManager.AddLogMessageWriterCreators(settings.LogMessageWriterCreators.ToArray());
            }

            builder.Associate(WellKnownAssociations.LoggerManager, loggerManager);
            builder.Bindings.Bind<ILoggerManager>().To(loggerManager);
            builder.Bindings.Bind(typeof(ILogger<>)).To(context => loggerManager.CreateLogger(context.Service.GetGenericArguments()[0]));
            builder.Bindings.Bind<ILogger>().To(() => loggerManager.CreateLogger<UnknownLogMessageSource>());

            var logger = loggerManager.CreateLogger<Logging>();
            logger.Information($"<********* BOOTSTAGE : Logging *********>");

            var executionContextLogger = loggerManager.CreateLogger<ExecutionContextManager>();

            ExecutionContextManager.SetInitialExecutionContext(executionContextLogger);

            builder.Bindings.Bind<IExecutionContextManager>().To(new ExecutionContextManager(executionContextLogger));
        }
    }
}
