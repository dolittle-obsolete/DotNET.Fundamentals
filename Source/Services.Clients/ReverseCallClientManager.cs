// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Google.Protobuf;
using Grpc.Core;
using grpc = contracts::Dolittle.Services.Contracts;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallClientManager"/>.
    /// </summary>
    public class ReverseCallClientManager : IReverseCallClientManager
    {
        /// <inheritdoc/>
        public Task Handle<TResponse, TRequest>(
            AsyncDuplexStreamingCall<TResponse, TRequest> call,
            Func<TResponse, grpc.ReverseCallResponseContext> getResponseContext,
            Action<TResponse, grpc.ReverseCallResponseContext> setResponseContext,
            Func<TRequest, grpc.ReverseCallRequestContext> getRequestContext,
            Func<ReverseCall<TResponse, TRequest>, Task> callback,
            CancellationToken token)
            where TResponse : IMessage
            where TRequest : IMessage
        {
            return Task.Run(
                async () =>
                {
                    while (await call.ResponseStream.MoveNext(token).ConfigureAwait(false))
                    {
                        var request = call.ResponseStream.Current;
                        var callId = getRequestContext(request).CallId.To<ReverseCallId>();
                        var reverseCall = new ReverseCall<TResponse, TRequest>(call.ResponseStream.Current, call.RequestStream, callId, getResponseContext, setResponseContext);
                        await callback(reverseCall).ConfigureAwait(false);
                    }
                }, token);
        }
    }
}