// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Services.Contracts;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallClientManagers" />.
    /// </summary>
    public class ReverseCallClientManagers : IReverseCallClientManagers
    {
        readonly IExecutionContextManager _executionContextManager;
        readonly ILoggerManager _loggerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallClientManagers"/> class.
        /// </summary>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="loggerManager">The <see cref="ILoggerManager" />.</param>
        public ReverseCallClientManagers(IExecutionContextManager executionContextManager, ILoggerManager loggerManager)
        {
            _executionContextManager = executionContextManager;
            _loggerManager = loggerManager;
        }

        /// <inheritdoc/>
        public IReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse> GetFor<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>(
            Func<AsyncDuplexStreamingCall<TClientMessage, TServerMessage>> establishConnection,
            Action<TConnectArguments, ReverseCallArgumentsContext> setConnectArgumentsContext,
            Action<TClientMessage, TConnectArguments> setConnectArguments,
            Func<TServerMessage, TConnectResponse> getConnectResponse,
            Func<TServerMessage, TRequest> getRequest,
            Func<TRequest, ReverseCallRequestContext> getRequestContext,
            Action<TResponse, ReverseCallResponseContext> setResponseContext,
            Action<TClientMessage, TResponse> setResponse)
            where TClientMessage : IMessage, new()
            where TServerMessage : IMessage, new()
            where TConnectArguments : class
            where TConnectResponse : class
            where TRequest : class
            where TResponse : class =>
                new ReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>(
                    establishConnection(),
                    setConnectArgumentsContext,
                    setConnectArguments,
                    getConnectResponse,
                    getRequest,
                    getRequestContext,
                    setResponseContext,
                    setResponse,
                    _executionContextManager,
                    _loggerManager.CreateLogger<ReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>>());
    }
}