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

namespace Dolittle.Booting
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
        public BootloaderResult Start()
        {
            var bootStages = new BootStages();
            bootStages.Perform(_boot);

            return null;
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
        /// Start booting
        /// </summary>
        public BootloaderResult Start()
        {

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

#endif                   

    }
}
