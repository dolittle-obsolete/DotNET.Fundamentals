/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using Dolittle.Applications;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{

    /// <summary>
    /// Represents a <see cref="IExecutionContext"/>
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContext"/>
        /// </summary>
        /// <param name="application"><see cref="Application"/> that is currently executing</param>
        /// <param name="boundedContext"><see cref="BoundedContext"/> that is currently executing</param>
        /// <param name="tenant"><see cref="TenantId"/> that is currently part of the <see cref="IExecutionContext"/></param>
        /// <param name="environment"><see cref="Environment"/> for this <see cref="IExecutionContext"/></param>
        /// <param name="correlationId"><see cref="CorrelationId"/> for this <see cref="IExecutionContext"/></param>
        /// <param name="principal"><see cref="ClaimsPrincipal"/> to populate with</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> for the <see cref="ExecutionContext"/></param>
        public ExecutionContext(
            Application application,
            BoundedContext boundedContext,
            TenantId tenant,
            Environment environment,
            CorrelationId correlationId,
            ClaimsPrincipal principal,
            CultureInfo cultureInfo)
        {
            Application = application;
            BoundedContext = boundedContext;
            Tenant = tenant;
            Environment = environment;
            CorrelationId = correlationId;
            Principal = principal;
            Culture = cultureInfo;
        }

        /// <inheritdoc/>
        public Application Application { get; }

        /// <inheritdoc/>
        public BoundedContext BoundedContext { get; }

        /// <inheritdoc/>
        public TenantId Tenant { get; }

        /// <inheritdoc/>
        public Environment Environment { get; }

        /// <inheritdoc/>
        public CorrelationId CorrelationId { get; }

        /// <inheritdoc/>
        public ClaimsPrincipal Principal { get; }

        /// <inheritdoc/>
        public CultureInfo Culture { get; }
    }
}
