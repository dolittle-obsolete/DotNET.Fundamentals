/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Reperesents an implementation of <see cref="IPolicy"/>
    /// </summary>
    public class Policy : IPolicy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Policy"/>
        /// </summary>
        /// <param name="underlyingPolicy">The underlying <see cref="Polly.Policy"/></param>
        public Policy(Polly.Policy underlyingPolicy)
        {
            UnderlyingPolicy = underlyingPolicy;
        }

        /// <summary>
        /// Gets the underlying <see cref="Polly.Policy">policy</see>
        /// </summary>
        public Polly.Policy UnderlyingPolicy { get; }

        /// <inheritdoc/>
        public void Execute(Action action)
        {
            UnderlyingPolicy.Execute(action);
        }

        /// <inheritdoc/>
        public TResult Execute<TResult>(Func<TResult> action)
        {
            return UnderlyingPolicy.Execute(action);
        }
    }
}