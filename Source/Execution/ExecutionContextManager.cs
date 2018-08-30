/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using System.Threading;
using Dolittle.Applications;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionContextManager"/>
    /// </summary>
    public class ExecutionContextManager : IExecutionContextManager
    {
        static AsyncLocal<IExecutionContext> _executionContext = new AsyncLocal<IExecutionContext>();

        Application _application;
        BoundedContext _boundedContext;
        Environment _environment;


        /// <inheritdoc/>
        public IExecutionContext Current
        {
            get 
            {
                var context = _executionContext.Value; 
                if( context == null ) throw new ExecutionContextNotSet();
                return context;
            }
            set {  _executionContext.Value = value; }
        }

        /// <inheritdoc/>
        public void SetConstants(Application application, BoundedContext boundedContext, Environment environment)
        {
            _application = application;
            _boundedContext = boundedContext;
            _environment = environment;
        }


        /// <inheritdoc/>
        public IExecutionContext GetFor(CorrelationId correlationId, TenantId tenant, ClaimsPrincipal principal)
        {
            return new ExecutionContext(
                _application, 
                _boundedContext, 
                tenant, 
                _environment, 
                correlationId, 
                principal, 
                CultureInfo.CurrentCulture);
        }
    }
}