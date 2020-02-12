// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a dispatcher that is capable tracking calls from server to client.
    /// </summary>
    /// <typeparam name="TResponse">Type of <see cref="IMessage"/> for the responses from the client.</typeparam>
    /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
    public interface IReverseCallDispatcher<TResponse, TRequest>
        where TResponse : IMessage
        where TRequest : IMessage
    {
        /// <summary>
        /// Dispatch a call to the client.
        /// </summary>
        /// <param name="request">Request <see cref="IMessage"/>.</param>
        /// <param name="callback">Callback for getting the <see cref="IMessage">response</see>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Call(TRequest request, Func<TResponse, Task> callback);

        /// <summary>
        /// Wait till client is disconnected. This will block.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task WaitTillDisconnected();
    }
}