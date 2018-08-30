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
    /// Defines the execution context in which things are within
    /// For instance, any commands coming into the system will be in the context of an execution context
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets the <see cref="Application"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        Application Application { get; }

        /// <summary>
        /// Gets the <see cref="BoundedContext"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        BoundedContext BoundedContext { get; }

        /// <summary>
        /// Gets the <see cref="TenantId"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        TenantId Tenant { get; }

        /// <summary>
        /// Gets the <see cref="Environment"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        /// Gets the <see cref="CorrelationId"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="ClaimsPrincipal"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        ClaimsPrincipal Principal { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the <see cref="IExecutionContext">execution context</see>
        /// </summary>
        CultureInfo Culture { get; }
    }
}
