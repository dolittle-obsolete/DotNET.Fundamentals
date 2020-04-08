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
        Task Call(TRequest request, Action<TResponse> callback);

        /// <summary>
        /// Dispatch a call to the client.
        /// </summary>
        /// <param name="request">Request <see cref="IMessage"/>.</param>
        /// <param name="callback">Callback for getting the <see cref="IMessage">response</see>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Call(TRequest request, Func<TResponse, Task> callback);

        /// <summary>
        /// Starts handling the reverse calls and returns a task that represents the processing of these calls.
        /// </summary>
        /// <remarks>
        /// If the handling of the calls finishes all future calls to this method will return the same task as the first time.
        /// If this task throws an <see cref="Exception" /> then all future calls to this method will throw the same <see cref="Exception" />.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task HandleCalls();
    }
}