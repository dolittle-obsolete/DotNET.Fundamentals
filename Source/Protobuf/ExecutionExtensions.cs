// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Globalization;
using Dolittle.ApplicationModel;
using Dolittle.Execution;
using Dolittle.Tenancy;
using grpc = contracts::Dolittle.Execution.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common execution types.
    /// </summary>
    public static class ExecutionExtensions
    {
        /// <summary>
        /// Convert a <see cref="ExecutionContext"/> to <see cref="grpc.ExecutionContext"/>.
        /// </summary>
        /// <param name="executionContext"><see cref="ExecutionContext"/> to convert from.</param>
        /// <returns>Converted <see cref="grpc.ExecutionContext"/>.</returns>
        public static grpc.ExecutionContext ToProtobuf(this ExecutionContext executionContext)
        {
            var message = new grpc.ExecutionContext
                {
                    MicroserviceId = executionContext.Microservice.ToProtobuf(),
                    TenantId = executionContext.Tenant.ToProtobuf(),
                    CorrelationId = executionContext.CorrelationId.ToProtobuf(),
                    Environment = executionContext.Environment,
                    Version = executionContext.Version.ToProtobuf()
                };
            message.Claims.AddRange(executionContext.Claims.ToProtobuf());

            return message;
        }

        /// <summary>
        /// Convert a <see cref="grpc.ExecutionContext"/> to <see cref="ExecutionContext"/>.
        /// </summary>
        /// <param name="executionContext"><see cref="grpc.ExecutionContext"/> to convert from.</param>
        /// <returns>Converted <see cref="ExecutionContext"/>.</returns>
        public static ExecutionContext ToExecutionContext(this grpc.ExecutionContext executionContext) =>
            new ExecutionContext(
                executionContext.MicroserviceId.To<Microservice>(),
                executionContext.TenantId.To<TenantId>(),
                executionContext.Version.ToVersion(),
                executionContext.Environment,
                executionContext.CorrelationId.To<CorrelationId>(),
                executionContext.Claims.ToClaims(),
                CultureInfo.InvariantCulture);
    }
}