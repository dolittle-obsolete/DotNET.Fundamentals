// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents a null implementation of <see cref="IAsyncPolicy"/>.
    /// </summary>
    /// <remarks>
    /// This policy does nothing, just passes through the calls.
    /// If no default policy is defined, this is the policy that typically will be used as the
    /// default policy.
    /// </remarks>
    public class PassThroughAsyncPolicy : IAsyncPolicy
    {
        /// <inheritdoc/>
        public Task ExecuteAsync(Func<Task> action) => ExecuteAsync(_ => action(), CancellationToken.None);

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken) => ExecuteAsync(action, false, CancellationToken.None);

        /// <inheritdoc/>
        public async Task ExecuteAsync(Func<CancellationToken, Task> action, bool continueOnCapturedContext, CancellationToken cancellationToken) => await action(cancellationToken).ConfigureAwait(continueOnCapturedContext);

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action) => ExecuteAsync(_ => action(), CancellationToken.None);

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> action, CancellationToken cancellationToken) => ExecuteAsync(action, false, CancellationToken.None);

        /// <inheritdoc/>
        public async Task<TResult> ExecuteAsync<TResult>(Func<CancellationToken, Task<TResult>> action, bool continueOnCapturedContext, CancellationToken cancellationToken) => await action(cancellationToken).ConfigureAwait(continueOnCapturedContext);
    }
}