// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using Dolittle.Applications;
using Dolittle.Concepts;
using Dolittle.Security;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents a <see cref="ExecutionContext"/>.
    /// </summary>
    public class ExecutionContext : Value<ExecutionContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        /// <param name="application"><see cref="Application"/> that is currently executing.</param>
        /// <param name="microservice"><see cref="Applications.Microservice"/> that is currently executing.</param>
        /// <param name="tenant"><see cref="TenantId"/> that is currently part of the <see cref="ExecutionContext"/>.</param>
        /// <param name="environment"><see cref="Environment"/> for this <see cref="ExecutionContext"/>.</param>
        /// <param name="correlationId"><see cref="CorrelationId"/> for this <see cref="ExecutionContext"/>.</param>
        /// <param name="claims"><see cref="Claims"/> to populate with.</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> for the <see cref="ExecutionContext"/>.</param>
        public ExecutionContext(
            Application application,
            Microservice microservice,
            TenantId tenant,
            Environment environment,
            CorrelationId correlationId,
            Claims claims,
            CultureInfo cultureInfo)
        {
            Application = application;
            Microservice = microservice;
            Tenant = tenant;
            Environment = environment;
            CorrelationId = correlationId;
            Claims = claims;
            Culture = cultureInfo;
        }

        /// <summary>
        /// Gets the <see cref="Application"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public Application Application { get; }

        /// <summary>
        /// Gets the <see cref="Microservice"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public Microservice Microservice { get; }

        /// <summary>
        /// Gets the <see cref="TenantId"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="Environment"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets the <see cref="CorrelationId"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="Claims"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public Claims Claims { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the <see cref="ExecutionContext">execution context</see>.
        /// </summary>
        public CultureInfo Culture { get; }
    }
}
