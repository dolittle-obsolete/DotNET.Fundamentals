// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace Dolittle.Async
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class TryTaskAwaiter<TResult> : INotifyCompletion
    {
        
        public bool IsCompleted { get; }

        public Try<TResult> GetResult()
        {

        }

        public void OnCompleted(Action completion)
        {
            completion();
        }
    }
}