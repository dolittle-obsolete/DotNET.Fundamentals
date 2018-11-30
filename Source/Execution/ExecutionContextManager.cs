/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using Dolittle.Applications;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Security;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionContextManager"/>
    /// </summary>
    [Singleton]
    public class ExecutionContextManager : IExecutionContextManager
    {
        static AsyncLocal<ExecutionContext> _executionContext = new AsyncLocal<ExecutionContext>();

        static bool _initialExecutionContextSet = false;

        readonly ILogger _logger;

        Application _application;
        BoundedContext _boundedContext;
        Environment _environment;


        /// <summary>
        /// Initializes a new instance of <see cref="ILogger"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ExecutionContextManager(ILogger logger)
        {
            _logger = logger;
            _application = Application.NotSet;
            _boundedContext = BoundedContext.NotSet;
            _environment = Environment.Undetermined;
        }

        /// <summary>
        /// Set the initial <see cref="ExecutionContext"/>
        /// </summary>
        /// <remarks>
        /// This can only be called once per process and is typically called by entrypoints into Dolittle itself.
        /// </remarks>
        public static ExecutionContext SetInitialExecutionContext([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
        {
            Logger.Internal.Trace($"Setting initial execution context - called from: ({filePath}, {lineNumber}, {member}) ", filePath, lineNumber, member);
            if( _initialExecutionContextSet ) throw new InitialExecutionContextHasAlreadyBeenSet();

            _initialExecutionContextSet = true;

            _executionContext.Value = new ExecutionContext(
                Application.NotSet,
                BoundedContext.NotSet,
                TenantId.System,
                Environment.Undetermined,
                CorrelationId.System,
                Claims.Empty,
                CultureInfo.InvariantCulture);

            return _executionContext.Value;
        }


        /// <inheritdoc/>
        public ExecutionContext Current
        {
            get 
            {
                var context = _executionContext.Value; 
                if( context == null ) throw new ExecutionContextNotSet();
                return context;
            }
            private set {  _executionContext.Value = value; }
        }

        /// <inheritdoc/>
        public void SetConstants(Application application, BoundedContext boundedContext, Environment environment)
        {
            _application = application;
            _boundedContext = boundedContext;
            _environment = environment;
        }

        /// <inheritdoc/>
        public ExecutionContext System(string filePath, int lineNumber, string member)
        {
            return CurrentFor(TenantId.System, CorrelationId.System, filePath, lineNumber, member);
        }

        /// <inheritdoc/>
        public ExecutionContext System(CorrelationId correlationId, string filePath, int lineNumber, string member)
        {
            return CurrentFor(TenantId.System, correlationId, filePath, lineNumber, member);
        }

        /// <inheritdoc/>
        public ExecutionContext CurrentFor(TenantId tenant, string filePath, int lineNumber, string member)
        {
            return CurrentFor(tenant, CorrelationId.New(), Claims.Empty, filePath, lineNumber, member);
        }

        /// <inheritdoc/>
        public ExecutionContext CurrentFor(TenantId tenant, CorrelationId correlationId,string filePath, int lineNumber, string member)
        {
            return CurrentFor(tenant, correlationId, Claims.Empty, filePath, lineNumber, member);
        }

        /// <inheritdoc/>
        public ExecutionContext CurrentFor(TenantId tenant, CorrelationId correlationId, Claims claims, string filePath, int lineNumber, string member)
        {
            var executionContext = new ExecutionContext(
                _application, 
                _boundedContext, 
                tenant, 
                _environment, 
                correlationId, 
                claims, 
                CultureInfo.CurrentCulture);

            return CurrentFor(executionContext, filePath, lineNumber, member);
        }


        /// <inheritdoc/>
        public ExecutionContext CurrentFor(ExecutionContext context, string filePath, int lineNumber, string member)
        {
            _logger.Trace($"Setting execution context ({context}) - from: ({filePath}, {lineNumber}, {member}) ", filePath, lineNumber, member);
            Current = context;
            return context;
        }
    }
}