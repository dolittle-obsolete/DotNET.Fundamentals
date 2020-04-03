// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Defines a client manager for reverse calls coming from server to client.
    /// </summary>
    public interface IReverseCallClientManager
    {
        /// <summary>
        /// Handle a call.
        /// </summary>
        /// <param name="call"><see cref="AsyncDuplexStreamingCall{TResponse, TRequest}">Call</see> to handle.</param>
        /// <param name="responseProperty">An <see cref="Expression{T}"/> for describing what property on response message that will hold the unique call identifier.</param>
        /// <param name="requestProperty">An <see cref="Expression{T}"/> for describing what property on request message that will hold the unique call identifier.</param>
        /// <param name="callback">The <see cref="Func{T1, TOut}">callback</see> for requests coming from server.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <typeparam name="TResponse">Type of <see cref="IMessage"/> for the responses from the client.</typeparam>
        /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Handle<TResponse, TRequest>(
            AsyncDuplexStreamingCall<TResponse, TRequest> call,
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            Func<ReverseCall<TResponse, TRequest>, Task> callback,
            CancellationToken token = default)
            where TResponse : IMessage
            where TRequest : IMessage;
    }
}