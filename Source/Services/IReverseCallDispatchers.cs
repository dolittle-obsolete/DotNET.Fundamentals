// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services
{
    /// <summary>
    /// Defines a system that provides trackers for reverse calls from server to client.
    /// </summary>
    public interface IReverseCallDispatchers
    {
        /// <summary>
        /// Get a <see cref="IReverseCallDispatcher{TResponse, TRequest}"/> for specific request and response streams.
        /// </summary>
        /// <param name="responseStream">The <see cref="IAsyncStreamReader{T}"/> for responses coming from the client.</param>
        /// <param name="requestStream">The <see cref="IServerStreamWriter{T}"/> for requests going to the client.</param>
        /// <param name="context">Original <see cref="ServerCallContext"/>.</param>
        /// <param name="responseContextProperty">An <see cref="Expression{T}"/> for describing what property on response message that will hold the <see cref="Contracts.ReverseCallResponseContext" />.</param>
        /// <param name="requestContextProperty">An <see cref="Expression{T}"/> for describing what property on request message that will hold the <see cref="Contracts.ReverseCallRequestContext" />.</param>
        /// <typeparam name="TResponse">Type of <see cref="IMessage"/> for the responses from the client.</typeparam>
        /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
        /// <returns>A <see cref="IReverseCallDispatcher{TResponse, TRequest}"/>.</returns>
        IReverseCallDispatcher<TResponse, TRequest> GetDispatcherFor<TResponse, TRequest>(
            IAsyncStreamReader<TResponse> responseStream,
            IServerStreamWriter<TRequest> requestStream,
            ServerCallContext context,
            Expression<Func<TResponse, Contracts.ReverseCallResponseContext>> responseContextProperty,
            Expression<Func<TRequest, Contracts.ReverseCallRequestContext>> requestContextProperty)
            where TResponse : IMessage
            where TRequest : IMessage;
    }
}