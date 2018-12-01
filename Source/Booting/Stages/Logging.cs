/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
    /// Represents the <see cref="BootStage.Logging"/> stage of booting
    /// </summary>
    public class Logging : ICanPerformBootStage<LoggingSettings>
    {
        readonly AsyncLocal<LoggingContext>  _currentLoggingContext = new AsyncLocal<LoggingContext>();    
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

            var loggerFactory = settings.LoggerFactory;
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();

            _isProduction = environment == Environment.Production; 

            var logAppender = settings.LogAppender;
            
            if( logAppender == null ) logAppender = _isProduction?
                (ILogAppender)new JsonLogAppender(GetCurrentLoggingContext):
                (ILogAppender)new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory);

            var logAppenders = Dolittle.Logging.Bootstrap.Boot.Start(loggerFactory, logAppender, entryAssembly);
            Dolittle.Logging.ILogger logger = new Logger(logAppenders);
            builder.Associate(WellKnownAssociations.Logger, logger);

            _executionContextManager = new ExecutionContextManager(logger);
            

            builder.Bindings.Bind<IExecutionContextManager>().To(_executionContextManager);
        }

        LoggingContext GetCurrentLoggingContext()
        {
            Dolittle.Execution.ExecutionContext executionContext = null;


            if (LoggingContextIsSet())
            {
                if (_executionContextManager != null) SetLatestLoggingContext();
                return _currentLoggingContext.Value;
            }
            
            if( _executionContextManager != null ) executionContext = _executionContextManager.Current;
            else executionContext = _initialExecutionContext;

            var loggingContext = CreateLoggingContextFrom(executionContext);
            _currentLoggingContext.Value = loggingContext;

            return loggingContext;
        }

        bool LoggingContextIsSet() => 
            _currentLoggingContext != null && _currentLoggingContext.Value != null;

        void SetLatestLoggingContext() => 
            _currentLoggingContext.Value = CreateLoggingContextFrom(_executionContextManager.Current);
            
        
        LoggingContext CreateLoggingContextFrom(Dolittle.Execution.ExecutionContext executionContext) =>
            new LoggingContext {
                Application = executionContext.Application,
                BoundedContext = executionContext.BoundedContext,
                CorrelationId = executionContext.CorrelationId,
                Environment = executionContext.Environment,
                TenantId = executionContext.Tenant
            };
    }
}
