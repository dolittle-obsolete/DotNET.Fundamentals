// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Logging;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallDispatchers"/>.
    /// </summary>
    public class ReverseCallDispatchers : IReverseCallDispatchers
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallDispatchers"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ReverseCallDispatchers(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public IReverseCallDispatcher<TResponse, TRequest> GetDispatcherFor<TResponse, TRequest>(
            IAsyncStreamReader<TResponse> responseStream,
            IServerStreamWriter<TRequest> requestStream,
            ServerCallContext context,
            Expression<Func<TResponse, Contracts.ReverseCallResponseContext>> responseContextProperty,
            Expression<Func<TRequest, Contracts.ReverseCallRequestContext>> requestContextProperty)
            where TResponse : IMessage
            where TRequest : IMessage
        {
            return new ReverseCallDispatcher<TResponse, TRequest>(
                responseStream,
                requestStream,
                context,
                responseContextProperty,
                requestContextProperty,
                _logger);
        }
    }
}