// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Security;
using Dolittle.Tenancy;
using Grpc.Core;
using Grpc.Core.Interceptors;
using grpc = Dolittle.Execution.Contracts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an <see cref="Interceptor"/> for dealing with setting the correct <see cref="ExecutionContext"/> for calls.
    /// </summary>
    public class ExecutionContextInterceptor : Interceptor
    {
        /// <summary>
        /// The key to be used in metadata headers for representing the <see cref="ExecutionContext"/>.
        /// </summary>
        public const string ExecutionContextKeyName = "executioncontext" + Metadata.BinaryHeaderSuffix;

        readonly IExecutionContextManager _executionContextManager;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextInterceptor"/> class.
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> for managing <see cref="ExecutionContext"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ExecutionContextInterceptor(
            IExecutionContextManager executionContextManager,
            ILogger logger)
        {
            _executionContextManager = executionContextManager;
            _logger = logger;
        }

        /// <inheritdoc/>
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.Options.Headers);
                return base.AsyncClientStreamingCall(context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.Options.Headers);
                return base.AsyncDuplexStreamingCall(context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.Options.Headers);
                return base.AsyncServerStreamingCall(request, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
                    where TRequest : class
                    where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.Options.Headers);
                return base.AsyncUnaryCall(request, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.Options.Headers);
                return base.BlockingUnaryCall(request, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.RequestHeaders);
                return base.ClientStreamingServerHandler(requestStream, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.RequestHeaders);
                return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                SetCurrentExecutionContext(context.Method, context.RequestHeaders);
                return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
            }
            catch (Exception ex)
            {
                HandleException(ex, context);
                throw;
            }
        }

        /// <inheritdoc/>
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
            where TRequest : class
            where TResponse : class
        {
            SetCurrentExecutionContext(context.Method, context.RequestHeaders);
            return base.UnaryServerHandler(request, context, continuation);
        }

        void SetCurrentExecutionContext(IMethod method, Metadata headers)
        {
            SetCurrentExecutionContext(method.FullName, headers);
        }

        void SetCurrentExecutionContext(string method, Metadata headers)
        {
            var executionContextEntry = headers.FirstOrDefault(_ => _.Key == ExecutionContextKeyName);
            if (executionContextEntry == default)
            {
                throw new ExecutionContextMissingInHeaderWhenAttemptingMethodInvocation(method);
            }

            var executionContext = grpc.ExecutionContext.Parser.ParseFrom(executionContextEntry.ValueBytes);

            var claims = executionContext.Claims.ToClaims();
            var application = executionContext.Application.To<Application>();
            var microservice = executionContext.Microservice.To<Microservice>();
            var tenant = executionContext.Tenant.To<TenantId>();
            var correlationId = executionContext.CorrelationId.To<CorrelationId>();

            _logger.Trace("Establishing execution context for '{method}' - Application: '{application}' Microservice: '{microservice}' TenantId: '{tenant}', CorrelationId: '{correlationId}'", method, application, microservice, tenant, correlationId);

            _executionContextManager.CurrentFor(
                application,
                microservice,
                tenant,
                correlationId,
                claims);
        }

        void HandleException<TRequest, TResponse>(Exception ex, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            _logger.Error(ex, "Problems handling '{method}' call", context.Method);
        }

        void HandleException(Exception ex, ServerCallContext context)
        {
            _logger.Error(ex, "Problems handling '{method}' call", context.Method);
        }
    }
}