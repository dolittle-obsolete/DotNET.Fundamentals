/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.DependencyInversion;
using Dolittle.Execution;
using Dolittle.Logging;
using Microsoft.Extensions.Logging;
using Environment = Dolittle.Execution.Environment;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the starting point - the actual boot of an application with configuration options
    /// for what is possible to configure before actual booting
    /// </summary>
    public class Bootloader
    {
        Assembly _entryAssembly;
        ICanProvideAssemblies   _assemblyProvider;
        Type _containerType;
        ILoggerFactory _loggerFactory = null;
        bool _isProduction = true;
    

        /// <summary>
        /// 
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
        public Bootloader SynchronousShceduling()
        {
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
        /// Start booting
        /// </summary>
        public void Start()
        {
            var l = _loggerFactory;
            var p = _isProduction;
            /*
            ExecutionContextManager.SetInitialExecutionContext();
            var loggerFactory = _loggerFactory;
            if( loggerFactory == null ) loggerFactory = new LoggerFactory();

            var environment = _isProduction?Environment.Production:Environment.Development;
            ILogAppender logAppender = _isProduction?
                new Json.JsonLogAppender(loggerFactory, GetCurrentLoggingContext, environment):
                new DefaultLogAppender(GetCurrentLoggingContext, logerFactory);


            var logAppenders = Dolittle.Logging.Bootstrap.Boot.Start(loggerFactory, logAppender, _entryAssembly);
            */
        }

        /*
        LoggingContext GetCurrentLoggingContext()
        {
            Dolittle.Execution.ExecutionContext executionContext = null;

            if( _executionContextManager == null && Container != null )
                _executionContextManager = Container.Get<IExecutionContextManager>();

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
        */
    }
}