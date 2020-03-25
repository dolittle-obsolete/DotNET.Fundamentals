// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Reflection;
using Google.Protobuf;
using Grpc.Core;

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
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            Func<ReverseCall<TResponse, TRequest>, Task> callback,
            CancellationToken token)
            where TResponse : IMessage
            where TRequest : IMessage
        {
            var responsePropertyInfo = responseProperty.GetPropertyInfo();
            var requestPropertyInfo = requestProperty.GetPropertyInfo();

            return Task.Run(
                async () =>
                {
                    while (await call.ResponseStream.MoveNext(token).ConfigureAwait(false))
                    {
                        var callNumber = (ulong)requestPropertyInfo.GetValue(call.ResponseStream.Current);

                        var reverseCall = new ReverseCall<TResponse, TRequest>(call.ResponseStream.Current, call.RequestStream, callNumber, responsePropertyInfo);
                        await callback(reverseCall).ConfigureAwait(false);
                    }
                }, token);
        }
    }
}