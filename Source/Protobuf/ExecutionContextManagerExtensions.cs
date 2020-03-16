// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.Tenancy;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Defines extension on top of <see cref="IExecutionContextManager"/>.
    /// </summary>
    public static class ExecutionContextManagerExtensions
    {
        /// <summary>
        /// Set current execution context for a Protobuf <see cref="Execution.Contracts.ExecutionContext"/>.
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> to extend.</param>
        /// <param name="executionContext"><see cref="Execution.Contracts.ExecutionContext"/> to set current.</param>
        public static void CurrentFor(this IExecutionContextManager executionContextManager, Execution.Contracts.ExecutionContext executionContext)
        {
            var application = executionContext.Application.To<Application>();
            var microservice = executionContext.Application.To<Microservice>();
            var tenant = executionContext.Tenant.To<TenantId>();
            var correlationId = executionContext.CorrelationId.To<CorrelationId>();
            var claims = executionContext.Claims.ToClaims();

            executionContextManager.CurrentFor(
                application,
                microservice,
                tenant,
                correlationId,
                claims);
        }
    }
}