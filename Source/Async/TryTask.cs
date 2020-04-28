// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace Dolittle.Async
{
    /// <summary>
    /// Represents a task type that returns <see cref="Try{TResult}" />.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    [AsyncMethodBuilder(typeof(TryTaskMethodBuilder<>))]
    public class TryTask<TResult>
        where TResult : class
    {
        public TryTaskAwaiter<TResult> GetAwaiter()
        {

        }
    }
}