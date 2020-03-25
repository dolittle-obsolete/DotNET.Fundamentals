// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Threading;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Logging.Json;
using Microsoft.Extensions.Logging;
using ExecutionContext = Dolittle.Execution.ExecutionContext;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.Logging"/> stage of booting.
    /// </summary>
    public class Logging : ICanPerformBootStage<LoggingSettings>
    {
        readonly AsyncLocal<LoggingContext> _currentLoggingContext = new AsyncLocal<LoggingContext>();
        ExecutionContext _initialExecutionContext;
        IExecutionContextManager _executionContextManager;
        bool _isProduction;

        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Logging;

        /// <inheritdoc/>
        public void Perform(LoggingSettings settings, IBootStageBuilder builder)
        {
            var entryAssembly = builder.GetAssociation(WellKnownAssociations.EntryAssembly) as Assembly;
            var environment = builder.GetAssociation(WellKnownAssociations.Environment) as Environment;
            _initialExecutionContext = ExecutionContextManager.SetInitialExecutionContext();

#pragma warning disable CA2000
            var loggerFactory = settings.LoggerFactory ?? new LoggerFactory();
#pragma warning restore CA2000

            _isProduction = environment == Environment.Production;

            var logAppender = settings.LogAppender;

            if (logAppender == null)
            {
                logAppender = (_isProduction && !settings.UseDefaultInAllEnvironments) ?
                    new JsonLogAppender(GetCurrentLoggingContext, loggerFactory) :
                    new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory) as ILogAppender;
            }

            var logAppenders = Dolittle.Logging.Bootstrap.Boot.Start(logAppender, entryAssembly);
            Dolittle.Logging.ILogger logger = settings.Logger ?? new Logger(logAppenders);
            logger.Information($"<********* BOOTSTAGE : Logging *********>");

            builder.Associate(WellKnownAssociations.Logger, logger);

            _executionContextManager = new ExecutionContextManager(logger);

            builder.Bindings.Bind<Dolittle.Logging.ILogger>().To(logger);
            builder.Bindings.Bind<ILoggerFactory>().To(loggerFactory);
            builder.Bindings.Bind<IExecutionContextManager>().To(_executionContextManager);
        }

        LoggingContext GetCurrentLoggingContext()
        {
            if (LoggingContextIsSet())
            {
                if (_executionContextManager != null) SetLatestLoggingContext();
                return _currentLoggingContext.Value;
            }

            ExecutionContext executionContext;
            if (_executionContextManager != null) executionContext = _executionContextManager.Current;
            else executionContext = _initialExecutionContext;

            var loggingContext = CreateLoggingContextFrom(executionContext);
            _currentLoggingContext.Value = loggingContext;

            return loggingContext;
        }

        bool LoggingContextIsSet() => _currentLoggingContext?.Value != null;

        void SetLatestLoggingContext() => _currentLoggingContext.Value = CreateLoggingContextFrom(_executionContextManager.Current);

        LoggingContext CreateLoggingContextFrom(ExecutionContext executionContext) =>
            new LoggingContext
            {
                Application = executionContext.Application,
                Microservice = executionContext.Microservice,
                CorrelationId = executionContext.CorrelationId,
                Environment = executionContext.Environment,
                TenantId = executionContext.Tenant
            };
    }
}
