// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Google.Protobuf;
using Grpc.Core;
using grpc = contracts::Dolittle.Services.Contracts;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents a call that is going from a server to a client and we need results from it.
    /// </summary>
    /// <typeparam name="TResponse">Type of <see cref="IMessage"/> the response is.</typeparam>
    /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
    public class ReverseCall<TResponse, TRequest>
        where TResponse : IMessage
        where TRequest : IMessage
    {
        readonly IClientStreamWriter<TResponse> _streamWriter;
        readonly Func<TResponse, grpc.ReverseCallResponseContext> _getResponseContext;
        readonly Action<TResponse, grpc.ReverseCallResponseContext> _setResponseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCall{TResponse, TRequest}"/> class.
        /// </summary>
        /// <param name="request">The request coming from the server.</param>
        /// <param name="streamWriter"><see cref="IClientStreamWriter{T}"/> for replying on.</param>
        /// <param name="callId">The identifier of the call.</param>
        /// <param name="getResponseContext">A <see cref="Func{T1, T2}"/> for getting the <see cref="grpc.ReverseCallResponseContext" />.</param>
        /// <param name="setResponseContext">An <see cref="Action{T1, T2}" /> for setting the <see cref="grpc.ReverseCallResponseContext" /> on the response.</param>
        public ReverseCall(
            TRequest request,
            IClientStreamWriter<TResponse> streamWriter,
            ReverseCallId callId,
            Func<TResponse, grpc.ReverseCallResponseContext> getResponseContext,
            Action<TResponse, grpc.ReverseCallResponseContext> setResponseContext)
        {
            Request = request;
            _streamWriter = streamWriter;
            CallId = callId;
            _getResponseContext = getResponseContext;
            _setResponseContext = setResponseContext;
        }

        /// <summary>
        /// Gets the unique <see cref="ReverseCallId" />.
        /// </summary>
        public ReverseCallId CallId { get; }

        /// <summary>
        /// Gets the request coming in from the server.
        /// </summary>
        public TRequest Request { get; }

        /// <summary>
        /// Reply on a call.
        /// </summary>
        /// <param name="reply"><see cref="IMessage"/> to reply with.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task Reply(TResponse reply)
        {
            var responseContext = _getResponseContext(reply);
            responseContext.CallId = CallId.ToProtobuf();
            _setResponseContext(reply, responseContext);
            return _streamWriter.WriteAsync(reply);
        }
    }
}