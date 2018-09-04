/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Claims;
using Dolittle.Applications;
using Dolittle.Concepts;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents a <see cref="ExecutionContext"/>
    /// </summary>
    public class ExecutionContext : Value<ExecutionContext>
    {
        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContext"/>
        /// </summary>
        /// <param name="application"><see cref="Application"/> that is currently executing</param>
        /// <param name="boundedContext"><see cref="BoundedContext"/> that is currently executing</param>
        /// <param name="tenant"><see cref="TenantId"/> that is currently part of the <see cref="ExecutionContext"/></param>
        /// <param name="environment"><see cref="Environment"/> for this <see cref="ExecutionContext"/></param>
        /// <param name="correlationId"><see cref="CorrelationId"/> for this <see cref="ExecutionContext"/></param>
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

        /// <summary>
        /// Gets the <see cref="Application"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public Application Application { get; }

        /// <summary>
        /// Gets the <see cref="BoundedContext"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public BoundedContext BoundedContext { get; }

        /// <summary>
        /// Gets the <see cref="TenantId"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="Environment"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets the <see cref="CorrelationId"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="ClaimsPrincipal"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public ClaimsPrincipal Principal { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the <see cref="ExecutionContext">execution context</see>
        /// </summary>
        public CultureInfo Culture { get; }    }
}
