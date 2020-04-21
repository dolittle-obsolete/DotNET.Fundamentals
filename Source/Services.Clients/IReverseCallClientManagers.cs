// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Services.Contracts;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Defines a system that knows about <see cref="IReverseCallClientManager{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}" />.
    /// </summary>
    public interface IReverseCallClientManagers
    {
        /// <summary>
        /// Gets a <see cref="IReverseCallClientManager{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}" /> that can mange the reverse calls.
        /// </summary>
        /// <typeparam name="TClientMessage">Type of the <see cref="IMessage">messages</see> that is sent from the client to the server.</typeparam>
        /// <typeparam name="TServerMessage">Type of the <see cref="IMessage">messages</see> that is sent from the server to the client.</typeparam>
        /// <typeparam name="TConnectArguments">Type of the arguments that are sent along with the initial Connect call.</typeparam>
        /// <typeparam name="TConnectResponse">Type of the response that is received after the initial Connect call.</typeparam>
        /// <typeparam name="TRequest">Type of the requests sent from the server to the client using <see cref="IReverseCallDispatcher{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}.Call"/>.</typeparam>
        /// <typeparam name="TResponse">Type of the responses received from the client using <see cref="IReverseCallDispatcher{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}.Call"/>.</typeparam>
        /// <param name="establishConnection">The <see cref="Func{TResult}" /> callback for connecting the client and establishing the <see cref="AsyncDuplexStreamingCall{TRequest, TResponse}" />.</param>
        /// <param name="setConnectArgumentsContext"><see cref="Action{T1, T2}" /> for setting the <see cref="ReverseCallArgumentsContext" /> on the connect arguments.</param>
        /// <param name="setConnectArguments"><see cref="Action{T1, T2}" /> for setting the connect arguments on the client message.</param>
        /// <param name="getConnectResponse"><see cref="Func{T1, TReturn}" /> for getting the connect response from the server message.</param>
        /// <param name="getRequest"><see cref="Func{T1, TReturn}" /> for getting the request from the server message.</param>
        /// <param name="getRequestContext"><see cref="Func{T1, TReturn}" /> for getting the <see cref="ReverseCallRequestContext" /> from the request.</param>
        /// <param name="setResponse"><see cref="Action{T1, T2}" /> for setting the response on the client message.</param>
        /// <returns>The <see cref="IReverseCallClientManager{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}" />.</returns>
        IReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse> GetFor<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>(
            Func<AsyncDuplexStreamingCall<TClientMessage, TServerMessage>> establishConnection,
            Action<TConnectArguments, ReverseCallArgumentsContext> setConnectArgumentsContext,
            Action<TClientMessage, TConnectArguments> setConnectArguments,
            Func<TServerMessage, TConnectResponse> getConnectResponse,
            Func<TServerMessage, TRequest> getRequest,
            Func<TRequest, ReverseCallRequestContext> getRequestContext,
            Action<TClientMessage, TResponse> setResponse)
            where TClientMessage : IMessage, new()
            where TServerMessage : IMessage, new()
            where TConnectArguments : class
            where TConnectResponse : class
            where TRequest : class
            where TResponse : class;
    }
}