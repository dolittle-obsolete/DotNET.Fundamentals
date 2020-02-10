// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Reflection;
using Google.Protobuf;
using Grpc.Core;

#pragma warning disable CA2008

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IReverseCallDispatcher{TResponse, TRequest}"/>.
    /// </summary>
    /// <typeparam name="TResponse">Type of <see cref="IMessage"/> for the responses from the client.</typeparam>
    /// <typeparam name="TRequest">Type of <see cref="IMessage"/> for the requests to the client.</typeparam>
    public class ReverseCallDispatcher<TResponse, TRequest> : IReverseCallDispatcher<TResponse, TRequest>, IDisposable
        where TResponse : IMessage
        where TRequest : IMessage
    {
        readonly object _lockObject = new object();
        readonly ConcurrentDictionary<ulong, Action<TResponse>> _requests = new ConcurrentDictionary<ulong, Action<TResponse>>();
        readonly ConcurrentDictionary<ulong, TResponse> _queuedResponses = new ConcurrentDictionary<ulong, TResponse>();
        readonly ConcurrentQueue<TResponse> _queuedForProcessing = new ConcurrentQueue<TResponse>();
        readonly ManualResetEventSlim _processEvent = new ManualResetEventSlim(false);
        readonly IAsyncStreamReader<TResponse> _responseStream;
        readonly IServerStreamWriter<TRequest> _requestStream;
        readonly ServerCallContext _context;
        readonly ILogger _logger;
        readonly PropertyInfo _responseProperty;
        readonly PropertyInfo _requestProperty;
        ulong _lastCallNumber = 0;
        ulong _lastResolvedCallNumber = 0;

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

            _context.CancellationToken.ThrowIfCancellationRequested();

            Task.Run(HandleResponse);
            Task.Run(HandleResponseProcessing);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _processEvent.Dispose();
        }

        /// <inheritdoc/>
        public void Call(TRequest request, Action<TResponse> callback)
        {
            lock (_lockObject)
            {
                _lastCallNumber++;
                _requestProperty.SetValue(request, _lastCallNumber);
                _requests[_lastCallNumber] = callback;

                _requestStream.WriteAsync(request).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task WaitTillDisconnected()
        {
            while (!_context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100).ConfigureAwait(false);
            }
        }

        void HandleResponseProcessing()
        {
            while (!_context.CancellationToken.IsCancellationRequested)
            {
                _processEvent.Wait();
                _processEvent.Reset();

                while (_queuedForProcessing.Count > 0)
                {
                    if (_queuedForProcessing.TryDequeue(out var response))
                    {
                        var callNumber = (ulong)_responseProperty.GetValue(response);
                        _requests[callNumber](response);
                        _requests.TryRemove(callNumber, out _);

                        ResolveFromQueue();
                    }
                }
            }
        }

        async Task HandleResponse()
        {
            while (await _responseStream.MoveNext(_context.CancellationToken).ConfigureAwait(false))
            {
                try
                {
                    lock (_lockObject)
                    {
                        TryResolve(_responseStream.Current);
                    }
                }
                catch
                {
                    if (_context.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                }
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
                _processEvent.Set();

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
                while (resolving)
                {
                    if (_queuedResponses.Count == 0) break;

                    var responses = _queuedResponses.Values.OrderBy(_ => (ulong)_responseProperty.GetValue(_));
                    resolving = TryResolve(responses.First());
                }
            }
        }
    }
}