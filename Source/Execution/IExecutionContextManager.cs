/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Security.Claims;
using Dolittle.Applications;
using Dolittle.Tenancy;

namespace Dolittle.Execution
{
    /// <summary>
    /// Defines the manager for <see cref="ExecutionContext"/>
    /// </summary>
    public interface IExecutionContextManager
    {
        /// <summary>
        /// Gets or sets the current <see cref="ExecutionContext"/>
        /// </summary>
        ExecutionContext Current { get; set; }

        /// <summary>
        /// Set constants that are used typically within a running process
        /// </summary>
        /// <param name="application">Which <see cref="Application"/> that is running</param>
        /// <param name="boundedContext">Which <see cref="BoundedContext"/> that is running</param>
        /// <param name="environment">Which <see cref="Environment"/> the system is running in</param>
         void SetConstants(Application application, BoundedContext boundedContext, Environment environment);


        /// <summary>
        /// Set current execution context for a <see cref="TenantId"/>
        /// </summary>
        /// <param name="tenant"><see cref="TenantId"/> to set for</param>
        /// <returns>Current <see cref="ExecutionContext"/></returns>
        ExecutionContext CurrentFor(TenantId tenant);

        /// <summary>
        /// Set current execution context for a <see cref="TenantId"/> with <see cref="CorrelationId"/> and <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="tenant"><see cref="TenantId"/> to set for</param>
        /// <param name="correlationId"><see cref="CorrelationId"/> to associate</param>
        /// <param name="principal"><see cref="ClaimsPrincipal"/> to assocatie</param>
        /// <returns>Current <see cref="ExecutionContext"/></returns>
        ExecutionContext CurrentFor(TenantId tenant, CorrelationId correlationId, ClaimsPrincipal principal);
    }
}
