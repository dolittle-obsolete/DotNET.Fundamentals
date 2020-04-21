// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.ApplicationModel;
using Dolittle.Execution;
using Dolittle.Tenancy;
using grpc = contracts::Dolittle.Execution.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Defines extension on top of <see cref="IExecutionContextManager"/>.
    /// </summary>
    public static class ExecutionContextManagerExtensions
    {
        /// <summary>
        /// Set current execution context for a Protobuf <see cref="grpc.ExecutionContext"/>.
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> to extend.</param>
        /// <param name="executionContext"><see cref="grpc.ExecutionContext"/> to set current.</param>
        public static void CurrentFor(this IExecutionContextManager executionContextManager, grpc.ExecutionContext executionContext)
        {
            var microservice = executionContext.MicroserviceId.To<Microservice>();
            var tenant = executionContext.TenantId.To<TenantId>();
            var correlationId = executionContext.CorrelationId.To<CorrelationId>();
            var claims = executionContext.Claims.ToClaims();

            executionContextManager.CurrentFor(
                microservice,
                tenant,
                correlationId,
                claims);
        }
    }
}