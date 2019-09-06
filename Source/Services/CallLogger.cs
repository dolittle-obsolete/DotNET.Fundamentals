/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a gRPC call interceptor for logging calls
    /// </summary>
    public class CallLogger : Interceptor
    {
        readonly ILogger _logger;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="CallLogger"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        /// <param name="serializer">JSON <see cref="ISerializer"/></param>
        public CallLogger(ILogger logger, ISerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.Debug($"GRPC Request - Method: {context.Method}, Data: {_serializer.ToJson(request)}");

            var response = await base.UnaryServerHandler(request, context, continuation);

            _logger.Debug($"GRPC Response - Method: {context.Method}, Data: {_serializer.ToJson(response)}");

            return response;
        }
    }
}