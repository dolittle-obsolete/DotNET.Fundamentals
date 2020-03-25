// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideClientMetadata"/>.
    /// </summary>
    public class ExecutionContextClientMetadataProvider : ICanProvideClientMetadata
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextClientMetadataProvider"/> class.
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for working with <see cref="ExecutionContext"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ExecutionContextClientMetadataProvider(
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<Metadata.Entry> Provide()
        {
            var current = _executionContextManager.Current;
            _logger.Trace($"Setting execution context on call - Application: '{current.Application}' Microservice: '{current.Microservice}' TenantId: {current.Tenant} - CorrelationId: {current.CorrelationId}");
            var currentMessage = current.ToProtobuf();
            var currentMessageAsBytes = currentMessage.ToByteArray();

            return new[]
            {
                new Metadata.Entry(ExecutionContextInterceptor.ExecutionContextKeyName, currentMessageAsBytes)
            };
        }
    }
}