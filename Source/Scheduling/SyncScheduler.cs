/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Collections;

namespace Dolittle.Scheduling
{
    /// <summary>
    /// Represents a <see cref="IScheduler"/> for scheduling synchronously
    /// </summary>
    public class SyncScheduler : IScheduler
    {
        /// <inheritdoc/>
        public void Perform(Action action)
        {
            action();
        }

        /// <inheritdoc/>
        public void PerformForEach<T>(IEnumerable<T> collection, Action<T> action)
        {
            collection.ForEach(action);
        }
    }
}