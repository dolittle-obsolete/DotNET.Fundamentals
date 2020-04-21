// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Services.Contracts;
using Google.Protobuf;
using Grpc.Core;

namespace Dolittle.Services.Clients
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallClientManager{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/>.
    /// </summary>
    /// <typeparam name="TClientMessage">Type of the <see cref="IMessage">messages</see> that is sent from the client to the server.</typeparam>
    /// <typeparam name="TServerMessage">Type of the <see cref="IMessage">messages</see> that is sent from the server to the client.</typeparam>
    /// <typeparam name="TConnectArguments">Type of the arguments that are sent along with the initial Connect call.</typeparam>
    /// <typeparam name="TConnectResponse">Type of the response that is received after the initial Connect call.</typeparam>
    /// <typeparam name="TRequest">Type of the requests sent from the server to the client using <see cref="IReverseCallDispatcher{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}.Call"/>.</typeparam>
    /// <typeparam name="TResponse">Type of the responses received from the client using <see cref="IReverseCallDispatcher{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}.Call"/>.</typeparam>
    public class ReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>
        : IDisposable, IReverseCallClientManager<TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse>
        where TClientMessage : IMessage, new()
        where TServerMessage : IMessage, new()
        where TConnectArguments : class
        where TConnectResponse : class
        where TRequest : class
        where TResponse : class
    {
        readonly IClientStreamWriter<TClientMessage> _clientToServer;
        readonly IAsyncStreamReader<TServerMessage> _serverToClient;
        readonly Action<TConnectArguments, ReverseCallArgumentsContext> _setConnectArgumentsContext;
        readonly Action<TClientMessage, TConnectArguments> _setConnectArguments;
        readonly Func<TServerMessage, TConnectResponse> _getConnectResponse;
        readonly Func<TServerMessage, TRequest> _getRequest;
        readonly Func<TRequest, ReverseCallRequestContext> _getRequestContext;
        readonly Action<TResponse, ReverseCallResponseContext> _setResponseContext;
        readonly Action<TClientMessage, TResponse> _setResponse;
        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;
        readonly SemaphoreSlim _writeResponseSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallClientManager{TClientMessage, TServerMessage, TConnectArguments, TConnectResponse, TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="streamingCall">The <see cref="AsyncDuplexStreamingCall{TRequest, TResponse}" />.</param>
        /// <param name="setConnectArgumentsContext"><see cref="Action{T1, T2}" /> for setting the <see cref="ReverseCallArgumentsContext" /> on the connect arguments.</param>
        /// <param name="setConnectArguments"><see cref="Action{T1, T2}" /> for setting the connect arguments on the client message.</param>
        /// <param name="getConnectResponse"><see cref="Func{T1, TReturn}" /> for getting the connect response from the server message.</param>
        /// <param name="getRequest"><see cref="Func{T1, TReturn}" /> for getting the request from the server message.</param>
        /// <param name="getRequestContext"><see cref="Func{T1, TReturn}" /> for getting the <see cref="ReverseCallRequestContext" /> from the request.</param>
        /// <param name="setResponseContext"><see cref="Action{T1, T2}" /> for setting the <see cref="ReverseCallResponseContext" /> on the response.</param>
        /// <param name="setResponse"><see cref="Action{T1, T2}" /> for setting the response on the client message.</param>
        /// <param name="executionContextManager">The <see cref="IExecutionContextManager" />.</param>
        /// <param name="logger">The <see cref="ILogger" />.</param>
        public ReverseCallClientManager(
            AsyncDuplexStreamingCall<TClientMessage, TServerMessage> streamingCall,
            Action<TConnectArguments, ReverseCallArgumentsContext> setConnectArgumentsContext,
            Action<TClientMessage, TConnectArguments> setConnectArguments,
            Func<TServerMessage, TConnectResponse> getConnectResponse,
            Func<TServerMessage, TRequest> getRequest,
            Func<TRequest, ReverseCallRequestContext> getRequestContext,
            Action<TResponse, ReverseCallResponseContext> setResponseContext,
            Action<TClientMessage, TResponse> setResponse,
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _clientToServer = streamingCall.RequestStream;
            _serverToClient = streamingCall.ResponseStream;
            _setConnectArgumentsContext = setConnectArgumentsContext;
            _setConnectArguments = setConnectArguments;
            _getConnectResponse = getConnectResponse;
            _getRequest = getRequest;
            _getRequestContext = getRequestContext;
            _setResponseContext = setResponseContext;
            _setResponse = setResponse;
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<TConnectResponse> Connect(TConnectArguments connectArguments, CancellationToken cancellationToken)
        {
            var callContext = new ReverseCallArgumentsContext
                {
                    ExecutionContext = _executionContextManager.Current.ToProtobuf()
                };
            _setConnectArgumentsContext(connectArguments, callContext);
            var message = new TClientMessage();
            _setConnectArguments(message, connectArguments);

            await _clientToServer.WriteAsync(message).ConfigureAwait(false);
            TConnectResponse response = null;
            if (await _serverToClient.MoveNext(cancellationToken).ConfigureAwait(false))
            {
                response = _getConnectResponse(_serverToClient.Current);
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task Handle(Func<TRequest, Task<TResponse>> callback, CancellationToken cancellationToken)
        {
            while (await _serverToClient.MoveNext(cancellationToken).ConfigureAwait(false))
            {
                _ = HandleRequest(callback, _getRequest(_serverToClient.Current), cancellationToken);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _writeResponseSemaphore.Dispose();
        }

        async Task HandleRequest(Func<TRequest, Task<TResponse>> callback, TRequest request, CancellationToken cancellationToken)
        {
            var requestContext = _getRequestContext(request);
            _executionContextManager.CurrentFor(requestContext.ExecutionContext);

            var response = await callback(request).ConfigureAwait(false);

            await _writeResponseSemaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                var responseContext = new ReverseCallResponseContext { CallId = requestContext.CallId };
                _setResponseContext(response, responseContext);
                var message = new TClientMessage();
                _setResponse(message, response);
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.Debug("Reverse call cancelled before request '{callId}' could be handled", requestContext.CallId.To<ReverseCallId>());
                    return;
                }

                await _clientToServer.WriteAsync(message).ConfigureAwait(false);
            }
            finally
            {
                _writeResponseSemaphore.Release();
            }
        }
    }
}