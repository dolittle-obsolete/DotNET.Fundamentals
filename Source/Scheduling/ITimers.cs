/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Timers;

namespace Dolittle.Scheduling
{
    /// <summary>
    /// Defines a system for working with <see cref="Timer">timers</see>
    /// </summary>
    public interface ITimers
    {
        /// <summary>
        /// Perform an action on every given interval in milliseconds
        /// </summary>
        /// <param name="interval">Interval in milliseconds</param>
        /// <param name="action"><see cref="Action"/> to perform</param>
        /// <returns><see cref="ITimer"/></returns>
        ITimer Every(double interval, Action action);
    }
}