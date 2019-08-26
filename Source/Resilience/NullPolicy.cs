/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents a null implementation of <see cref="IPolicy"/>
    /// </summary>
    public class NullPolicy : IPolicy
    {
        /// <inheritdoc/>
        public void Execute(Action action)
        {
            action();
        }

        /// <inheritdoc/>
        public TResult Execute<TResult>(Func<TResult> action)
        {
            return action();
        }
    }
}