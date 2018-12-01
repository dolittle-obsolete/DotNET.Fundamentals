/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.Time;
using Dolittle.Scheduling;
using Microsoft.Extensions.Logging;
using Dolittle.Logging;
using Dolittle.Logging.Json;
using System.Threading;
using ExecutionContext = Dolittle.Execution.ExecutionContext;
using System.Reflection;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.InitialSystem"/> stage of booting
    /// </summary>
    public class InitialSystem : ICanPerformBootStage<InitialSystemSettings>
    {
        readonly AsyncLocal<LoggingContext>  _currentLoggingContext = new AsyncLocal<LoggingContext>();    
        ExecutionContext _initialExecutionContext;
        IExecutionContextManager _executionContextManager;
        bool _isProduction;

        /// <inheritdoc/>
        public BootStage BootStage => BootStage.InitialSystem;

        /// <inheritdoc/>
        public void Perform(InitialSystemSettings settings, IBootStageBuilder builder)
        {
            var entryAssembly = settings.EntryAssembly ?? Assembly.GetEntryAssembly();
            var environment = settings.Environment ?? Environment.Production;
            var scheduler = settings.Scheduler ?? new AsyncScheduler();

            builder.Associate(WellKnownAssociations.EntryAssembly, entryAssembly);
            builder.Associate(WellKnownAssociations.Environment, environment);
            builder.Associate(WellKnownAssociations.Scheduler, scheduler);

            _initialExecutionContext = ExecutionContextManager.SetInitialExecutionContext();

            builder.Bindings.Bind<ISystemClock>().To(settings.SystemClock ?? typeof(SystemClock));;
            builder.Bindings.Bind<IFileSystem>().To(settings.FileSystem ?? typeof(FileSystem));
            builder.Bindings.Bind<IScheduler>().To(scheduler);
            builder.Bindings.Bind<Environment>().To(environment);

            var loggerFactory = settings.LoggerFactory;        
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();

            _isProduction = settings.Environment == Environment.Production; 

            var logAppender = settings.LogAppender;
            
            if( logAppender == null ) logAppender = _isProduction?
                (ILogAppender)new JsonLogAppender(GetCurrentLoggingContext):
                (ILogAppender)new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory);

            var logAppenders = Dolittle.Logging.Bootstrap.Boot.Start(loggerFactory, logAppender, settings.EntryAssembly);
            Logging.ILogger logger = new Logger(logAppenders);

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
