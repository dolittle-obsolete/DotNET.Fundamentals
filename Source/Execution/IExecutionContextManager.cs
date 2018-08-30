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
    /// Defines the manager for <see cref="IExecutionContext"/>
    /// </summary>
    public interface IExecutionContextManager
    {
        /// <summary>
        /// Gets or sets the current <see cref="IExecutionContext"/>
        /// </summary>
        IExecutionContext Current { get; set; }

        /// <summary>
        /// Set constants that are used typically within a running process
        /// </summary>
        /// <param name="application">Which <see cref="Application"/> that is running</param>
        /// <param name="boundedContext">Which <see cref="BoundedContext"/> that is running</param>
        /// <param name="environment">Which <see cref="Environment"/> the system is running in</param>
         void SetConstants(Application application, BoundedContext boundedContext, Environment environment);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="tenant"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        IExecutionContext GetFor(CorrelationId correlationId, TenantId tenant, ClaimsPrincipal principal);
    }
}
