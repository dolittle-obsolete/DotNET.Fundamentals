// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents an implementation of <see cref="IAsyncPolicy"/>.
    /// </summary>
    public class AsyncPolicy : IAsyncPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncPolicy"/> class.
        /// </summary>
        /// <param name="delegatedPolicy"><see cref="IAsyncPolicy"/> to delegate to.</param>
        public AsyncPolicy(IAsyncPolicy delegatedPolicy)
        {
            DelegatedPolicy = delegatedPolicy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncPolicy"/> class.
        /// </summary>
        /// <param name="underlyingPolicy">The underlying <see cref="Polly.Policy"/>.</param>
        public AsyncPolicy(Polly.IAsyncPolicy underlyingPolicy)
        {
            UnderlyingPolicy = underlyingPolicy;
        }

        /// <summary>
        /// Gets the underlying <see cref="Polly.Policy">policy</see>.
        /// </summary>
        public Polly.IAsyncPolicy UnderlyingPolicy { get; }

        /// <summary>
        /// Gets the delegated <see cref="IPolicy"/>.
        /// </summary>
        public IAsyncPolicy DelegatedPolicy { get; }

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<Task> action) => ExecuteAsync(_ => action(), CancellationToken.None);

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken) => ExecuteAsync(action, false, cancellationToken);

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<CancellationToken, Task> action, bool continueOnCapturedContext, CancellationToken cancellationToken)
        {
            if (DelegatedPolicy != null) return DelegatedPolicy.ExecuteAsync(action, continueOnCapturedContext, cancellationToken);
            else return UnderlyingPolicy.ExecuteAsync(action, cancellationToken, continueOnCapturedContext);
        }

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action) => ExecuteAsync(_ => action(), CancellationToken.None);

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken) => ExecuteAsync(action, false, cancellationToken);

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> action, bool continueOnCapturedContext, CancellationToken cancellationToken)
        {
            if (DelegatedPolicy != null) return DelegatedPolicy.ExecuteAsync(action, continueOnCapturedContext, cancellationToken);
            return UnderlyingPolicy.ExecuteAsync(action, cancellationToken, continueOnCapturedContext);
        }
    }
}