// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Reflection;
using Google.Protobuf;
using Grpc.Core;
using grpc = contracts::Dolittle.Services.Contracts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallDispatcher{TResponse, TRequest}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Type of <see cref="IMessage"/> for the responses from the client.</typeparam>
    /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
    public class ReverseCallDispatcher<TResponse, TRequest> : IReverseCallDispatcher<TResponse, TRequest>
        where TResponse : IMessage
        where TRequest : IMessage
    {
        readonly object _lockObject = new object();

        readonly ConcurrentDictionary<ReverseCallId, TaskCompletionSource<TResponse>> _calls = new ConcurrentDictionary<ReverseCallId, TaskCompletionSource<TResponse>>();
        readonly IAsyncStreamReader<TResponse> _responseStream;
        readonly IServerStreamWriter<TRequest> _requestStream;
        readonly ServerCallContext _context;
        readonly ILogger _logger;
        readonly Func<TResponse, grpc.ReverseCallResponseContext> _getResponseContext;
        readonly Func<TRequest, grpc.ReverseCallRequestContext> _getRequestContext;
        readonly PropertyInfo _requestContextProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallDispatcher{TResponse, TRequest}"/> class.
        /// </summary>
        /// <param name="responseStream">The <see cref="IAsyncStreamReader{T}"/> for responses coming from the client.</param>
        /// <param name="requestStream">The <see cref="IServerStreamWriter{T}"/> for requests going to the client.</param>
        /// <param name="context">Original <see cref="ServerCallContext"/>.</param>
        /// <param name="responseContextProperty">An <see cref="Expression{T}"/> for describing what property on response message that will hold the <see cref="grpc.ReverseCallResponseContext" />.</param>
        /// <param name="requestContextProperty">An <see cref="Expression{T}"/> for describing what property on request message that will hold the <see cref="grpc.ReverseCallRequestContext" />.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ReverseCallDispatcher(
            IAsyncStreamReader<TResponse> responseStream,
            IServerStreamWriter<TRequest> requestStream,
            ServerCallContext context,
            Expression<Func<TResponse, grpc.ReverseCallResponseContext>> responseContextProperty,
            Expression<Func<TRequest, grpc.ReverseCallRequestContext>> requestContextProperty,
            ILogger logger)
        {
            _responseStream = responseStream;
            _requestStream = requestStream;
            _context = context;
            _logger = logger;
            _getResponseContext = responseContextProperty.Compile();
            _getRequestContext = requestContextProperty.Compile();
            _requestContextProperty = requestContextProperty.GetPropertyInfo();

            Task.Run(HandleResponse);
        }

        /// <inheritdoc/>
        public Task<TResponse> Call(TRequest request)
        {
            lock (_lockObject)
            {
                var callId = new ReverseCallId { Value = Guid.NewGuid() };
                var requestContext = _getRequestContext(request);
                requestContext.CallId = callId.ToProtobuf();
                _requestContextProperty.SetValue(request, requestContext);
                _calls[callId] = new TaskCompletionSource<TResponse>();

                _logger.Trace("Dispatching reverse call with Call Id = '{callId}' Request = {request}", callId, request);
                _requestStream.WriteAsync(request).GetAwaiter().GetResult();

                return _calls[callId].Task;
            }
        }

        async Task HandleResponse()
        {
            try
            {
                while (!await _responseStream.MoveNext(_context.CancellationToken).ConfigureAwait(false))
                {
                    var response = _responseStream.Current;
                    var responseContext = _getResponseContext(response);
                    var callId = responseContext.CallId.To<ReverseCallId>();
                    if (!_calls.TryGetValue(callId, out var completionSource))
                    {
                        _logger.Warning("Received response with an unmapped reverse Call Id = '{callId}'. Discarding response", callId);
                    }
                    else
                    {
                        completionSource.SetResult(response);
                    }
                }

                CancelRemainingCalls();
            }
            catch (Exception ex)
            {
                CancelRemainingCalls(ex);
            }
        }

        void CancelRemainingCalls(Exception ex = null)
        {
            Action<TaskCompletionSource<TResponse>> cancelCall = null;
            if (ex == null)
            {
                _logger.Warning("Cancelling remaining uncompleted reverse calls");
                cancelCall = completionSource => completionSource.TrySetCanceled();
            }
            else
            {
                _logger.Warning(ex, "Reading from response stream failed. Cancelling remaining uncompleted reverse calls");
                cancelCall = completionSource => completionSource.TrySetException(ex);
            }

            foreach ((var callId, var completionSource) in _calls.Where(_ => !_.Value.Task.IsCompleted).ToList())
            {
                _logger.Trace("Cancelling uncompleted call with reverse call id '{callId}'", callId);
                cancelCall(completionSource);
            }
        }
    }
}