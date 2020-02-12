// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

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
        readonly PropertyInfo _responseProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCall{TResponse, TRequest}"/> class.
        /// </summary>
        /// <param name="request">The request coming from the server.</param>
        /// <param name="streamWriter"><see cref="IClientStreamWriter{T}"/> for replying on.</param>
        /// <param name="callNumber">The identifier of the call.</param>
        /// <param name="responseProperty"><see cref="PropertyInfo"/> for setting the call identifier on the response.</param>
        public ReverseCall(
            TRequest request,
            IClientStreamWriter<TResponse> streamWriter,
            ulong callNumber,
            PropertyInfo responseProperty)
        {
            Request = request;
            _streamWriter = streamWriter;
            CallNumber = callNumber;
            _responseProperty = responseProperty;
        }

        /// <summary>
        /// Gets the unique call number.
        /// </summary>
        public ulong CallNumber { get; }

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
            _responseProperty.SetValue(reply, CallNumber);
            return _streamWriter.WriteAsync(reply);
        }
    }
}