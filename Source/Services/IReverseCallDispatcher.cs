// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        /// <returns>A <see cref="Task"/> that, when resolved, returns the response.</returns>
        Task<TResponse> Call(TRequest request);

        /// <summary>
        /// Waits for the <see cref="IReverseCallDispatcher{TResponse, TRequest}" /> to finish dispatching calls.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task WaitTillCompleted();
    }
}