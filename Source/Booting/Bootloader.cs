/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Dolittle.Applications;
using Dolittle.Assemblies;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.Logging;
using Dolittle.Logging.Json;
using Dolittle.Scheduling;
using Microsoft.Extensions.Logging;
using Environment = Dolittle.Execution.Environment;
using ExecutionContext = Dolittle.Execution.ExecutionContext;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the starting point - the actual boot of an application with configuration options
    /// for what is possible to configure before actual booting
    /// </summary>
    public class Bootloader
    {
        readonly Boot _boot;

        /// <summary>
        /// Initializes a new instance of <see cref="Bootloader"/>
        /// </summary>
        /// <param name="boot"></param>
        public Bootloader(Boot boot)
        {
            _boot = boot;
        }

        /// <summary>
        /// Start booting
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// Configure boot
        /// </summary>
        /// <param name="builderDelegate">Builder delegete</param>
        /// <returns><see cref="Bootloader"/> for booting</returns>
        public static Bootloader Configure(Action<IBootBuilder> builderDelegate)
        {
            var builder = new BootBuilder();
            builderDelegate(builder);
            var boot = builder.Build();
            var bootLoader = new Bootloader(boot);
            return bootLoader;
        }

#if(false)
        Assembly _entryAssembly;
        ICanProvideAssemblies   _assemblyProvider;
        Type _containerType;
        ILoggerFactory _loggerFactory = null;
        bool _isProduction = true;

        ExecutionContext _initialExecutionContext;
        IExecutionContextManager _executionContextManager;
        IContainer _container;

        bool _skipBootProcedures = false;
        bool _synchronousScheduling = false;
        ILogAppender _logAppender;
        readonly AsyncLocal<LoggingContext>  _currentLoggingContext = new AsyncLocal<LoggingContext>();    

        /// <summary>
        /// Start configuring the <see cref="Bootloader"/>
        /// </summary>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public static Bootloader Configure()
        {
            var bootloader = new Bootloader();
            return bootloader;
        }

        /// <summary>
        /// Define which container to be used during application lifecycle
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IContainer">container</see></typeparam>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        /// <remarks>
        /// This is normally discovered using the interface <see cref="ICanProvideContainer"/>
        /// but in some cases you might need to be explicit, e.g. when you have a wrapper around
        /// the actual container.
        /// </remarks>
        public Bootloader UseContainer<T>() where T:IContainer
        {
            _containerType = typeof(T);
            return this;
        }

        /// <summary>
        /// Use a specific <see cref="ILoggerFactory"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader UseLoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            return this;
        }

        /// <summary>
        /// Use a specific <see cref="ILogAppender"/>
        /// </summary>
        /// <param name="logAppender"><see cref="ILogAppender"/> to use</param>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader UseLogAppender(ILogAppender logAppender)
        {
            _logAppender = logAppender;
            return this;
        }

        /// <summary>
        /// Run in development mode
        /// </summary>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader Development()
        {
            _isProduction = false;
            return this;
        }

        /// <summary>
        /// Run solution using synchronous scheduling
        /// </summary>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader SynchronousScheduling()
        {
            _synchronousScheduling = true;
            return this;
        }

        /// <summary>
        /// Specify entry assembly
        /// </summary>
        /// <param name="entryAssembly"><see cref="Assembly"/> which is considered the entry assembly</param>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader WithEntryAssembly(Assembly entryAssembly)
        {
            _entryAssembly = entryAssembly;
            return this;
        }

        /// <summary>
        /// Specify the known assemblies instead of discovering them
        /// </summary>
        /// <param name="assemblies"><see cref="IEnumerable{T}">Collection</see> of <see cref="AssemblyName"/> representing known assemblies</param>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader WithAssemblies(IEnumerable<AssemblyName> assemblies)
        {
            _assemblyProvider = new WellKnownAssembliesAssemblyProvider(assemblies);
            return this;
        }

        /// <summary>
        /// Tells the bootloader to skip any <see cref="ICanPerformBootProcedure">boot procedures</see>
        /// </summary>
        /// <returns>Chained <see cref="Bootloader"/> for configuration</returns>
        public Bootloader SkipBootprocedures()
        {
            _skipBootProcedures = true;
            return this;
        }

        /// <summary>
        /// Start booting
        /// </summary>
        public BootloaderResult Start()
        {
            /*
                Stages / Phase / Sequence:
                http://processors.wiki.ti.com/index.php/The_Boot_Process#

                0: System
                - FileSystem
                - SystemClock
                - Scheduler
                - Logging 

                1: Discovery
                - Assemblies
                - Types

                2: Configuration
                - Configuration objects

                3: Bindings
                - Discover through conventions
                - Discover through binding providers

                4: Boot procedures


                ICanRunBefore<BootStage.MasterBootRecord>
                
                public interface IBootStageConfiguration
                {

                }

                public interface ICanPerformBootStage
                {
                    BootStage BootStage { get; }
                }

                public interface ICanRunBeforeBootStage
                {
                    BootStage BootStage { get; }
                }

                public interface ICanRunAfterBootStage
                {
                    BootStage BootStage { get; }
                }

                public class BootInformation
                {
                    public BootStage CurrentStage { get; }
                    public bool IsBooting { get; }
                }
                
             */



            var l = _loggerFactory;
            var p = _isProduction;
            IScheduler scheduler = _synchronousScheduling?
                (IScheduler)new SyncScheduler():
                (IScheduler)new AsyncScheduler();
            
            _initialExecutionContext = ExecutionContextManager.SetInitialExecutionContext();
            var loggerFactory = _loggerFactory;
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();
            
            var environment = _isProduction?Environment.Production:Environment.Development;
            ILogAppender logAppender = _logAppender;
            
            if( logAppender == null ) logAppender = _isProduction?
                (ILogAppender)new JsonLogAppender(GetCurrentLoggingContext):
                (ILogAppender)new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory);

            var logAppenders = Dolittle.Logging.Bootstrap.Boot.Start(loggerFactory, logAppender, _entryAssembly);
            Logging.ILogger logger = new Logger(logAppenders);

            logger.Trace($"Using {scheduler.GetType().Name} as scheduler");

            var fileSystem = new FileSystem();

            var assemblies = Dolittle.Assemblies.Bootstrap.Boot.Start(logger, _entryAssembly, _assemblyProvider);
            var typeFinder = Dolittle.Types.Bootstrap.Boot.Start(assemblies, scheduler, _entryAssembly);
            
            var bindings = new[] {
                new BindingBuilder(Binding.For(typeof(Environment))).To(environment).Build(),
            };

            IBindingCollection resultingBindings;

            if( _containerType != null ) 
            {
                logger.Trace($"Starting DependencyInversion with predefined container type '{_containerType.AssemblyQualifiedName}'");
                resultingBindings = Dolittle.DependencyInversion.Bootstrap.Boot.Start(assemblies, typeFinder, scheduler, fileSystem, logger, _containerType, bindings);
            } 
            else 
            {
                var bootResult = Dolittle.DependencyInversion.Bootstrap.Boot.Start(assemblies, typeFinder, scheduler, fileSystem, logger, bindings);
                resultingBindings = bootResult.Bindings;
                _container = bootResult.Container;
                logger.Trace($"Using container of type '{_container.GetType().AssemblyQualifiedName}'");
            }

            var result = new BootloaderResult(_container, typeFinder, assemblies, resultingBindings);

            
            if( !_skipBootProcedures && _container != null ) 
            {
                logger.Trace($"Start boot procedures");
                Bootstrapper.Start(_container, logger);
            }

            if (_container != null) _container.Get<IExecutionContextManager>().SetConstants(_container.Get<Application>(), _container.Get<BoundedContext>(), environment);

            return result;
        }     

        
        LoggingContext GetCurrentLoggingContext()
        {
            Dolittle.Execution.ExecutionContext executionContext = null;

            if( _executionContextManager == null && _container != null )
                _executionContextManager = _container.Get<IExecutionContextManager>();

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
#endif                   

    }
}
