// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Reflection;
using Google.Protobuf;
using Grpc.Core;

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
        readonly ConcurrentDictionary<ulong, Func<TResponse, Task>> _requests = new ConcurrentDictionary<ulong, Func<TResponse, Task>>();
        readonly ConcurrentDictionary<ulong, TaskCompletionSource<object>> _callbackTasks = new ConcurrentDictionary<ulong, TaskCompletionSource<object>>();
        readonly ConcurrentDictionary<ulong, TResponse> _queuedResponses = new ConcurrentDictionary<ulong, TResponse>();
        readonly ConcurrentQueue<TResponse> _queuedForProcessing = new ConcurrentQueue<TResponse>();
        readonly IAsyncStreamReader<TResponse> _responseStream;
        readonly IServerStreamWriter<TRequest> _requestStream;
        readonly ServerCallContext _context;
        readonly ILogger _logger;
        readonly PropertyInfo _responseProperty;
        readonly PropertyInfo _requestProperty;
        readonly IEnumerable<Task> _tasks;
        readonly Task _handleResponse;
        ulong _lastCallNumber = 0;
        ulong _lastResolvedCallNumber = 0;
        bool _finishedHandlingResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallDispatcher{TResponse, TRequest}"/> class.
        /// </summary>
        /// <param name="responseStream">The <see cref="IAsyncStreamReader{T}"/> for responses coming from the client.</param>
        /// <param name="requestStream">The <see cref="IServerStreamWriter{T}"/> for requests going to the client.</param>
        /// <param name="context">Original <see cref="ServerCallContext"/>.</param>
        /// <param name="responseProperty">An <see cref="Expression{T}"/> for describing what property on response message that will hold the unique call identifier.</param>
        /// <param name="requestProperty">An <see cref="Expression{T}"/> for describing what property on request message that will hold the unique call identifier.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ReverseCallDispatcher(
            IAsyncStreamReader<TResponse> responseStream,
            IServerStreamWriter<TRequest> requestStream,
            ServerCallContext context,
            Expression<Func<TResponse, ulong>> responseProperty,
            Expression<Func<TRequest, ulong>> requestProperty,
            ILogger logger)
        {
            _responseStream = responseStream;
            _requestStream = requestStream;
            _context = context;
            _logger = logger;
            _responseProperty = responseProperty.GetPropertyInfo();
            _requestProperty = requestProperty.GetPropertyInfo();

            _tasks = new[]
                {
                    Task.Run(HandleResponse),
                    Task.Run(HandleResponseProcessing)
                };
            _handleResponse = Task.WhenAny(_tasks);
        }

        /// <inheritdoc/>
        public Task Call(TRequest request, Action<TResponse> callback)
        {
            return Call(request, response =>
            {
                callback(response);
                return Task.CompletedTask;
            });
        }

        /// <inheritdoc/>
        public Task Call(TRequest request, Func<TResponse, Task> callback)
        {
            lock (_lockObject)
            {
                _lastCallNumber++;
                _requestProperty.SetValue(request, _lastCallNumber);
                _requests[_lastCallNumber] = callback;

                var taskCompletionSource = new TaskCompletionSource<object>();
                _callbackTasks[_lastCallNumber] = taskCompletionSource;

                _requestStream.WriteAsync(request).GetAwaiter().GetResult();

                return taskCompletionSource.Task;
            }
        }

        /// <inheritdoc/>
        public async Task HandleCalls()
        {
            await _handleResponse.ConfigureAwait(false);
            var exception = _tasks.FirstOrDefault(_ => _.Exception != null)?.Exception;
            if (exception != null) throw exception;
        }

        async Task HandleResponseProcessing()
        {
            try
            {
                while (!_context.CancellationToken.IsCancellationRequested && !_finishedHandlingResponse)
                {
                    await Task.Delay(50).ConfigureAwait(false);

                    while (_queuedForProcessing.Count > 0)
                    {
                        if (_queuedForProcessing.TryDequeue(out var response))
                        {
                            var callNumber = (ulong)_responseProperty.GetValue(response);
                            await _requests[callNumber](response).ConfigureAwait(false);
                            _requests.TryRemove(callNumber, out _);
                            _callbackTasks[callNumber].SetResult(null);
                            _callbackTasks.TryRemove(callNumber, out _);

                            ResolveFromQueue();
                        }
                    }
                }
            }
            finally
            {
                _finishedHandlingResponse = true;
            }
        }

        async Task HandleResponse()
        {
            try
            {
                while (!_finishedHandlingResponse && await _responseStream.MoveNext(_context.CancellationToken).ConfigureAwait(false))
                {
                    lock (_lockObject)
                    {
                        TryResolve(_responseStream.Current);
                    }
                }
            }
            finally
            {
                _finishedHandlingResponse = true;
            }
        }

        bool TryResolve(TResponse response)
        {
            var callNumber = (ulong)_responseProperty.GetValue(response);
            var delta = callNumber - _lastResolvedCallNumber;
            if (delta == 1)
            {
                _lastResolvedCallNumber = callNumber;

                if (_queuedResponses.ContainsKey(callNumber))
                {
                    _queuedResponses.TryRemove(callNumber, out _);
                }

                _queuedForProcessing.Enqueue(response);

                return true;
            }
            else
            {
                _queuedResponses[callNumber] = _responseStream.Current;

                return false;
            }
        }

        void ResolveFromQueue()
        {
            if (_queuedResponses.Count > 0)
            {
                var resolving = true;
                while (!_context.CancellationToken.IsCancellationRequested && resolving)
                {
                    if (_queuedResponses.Count == 0) break;

                    var responses = _queuedResponses.Values.OrderBy(_ => (ulong)_responseProperty.GetValue(_));
                    resolving = TryResolve(responses.First());
                }
            }
        }
    }
}