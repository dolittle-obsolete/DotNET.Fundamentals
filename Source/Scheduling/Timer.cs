/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using SysTimer = System.Timers.Timer;

namespace Dolittle.Scheduling
{
    /// <summary>
    /// Represents an implementation of <see cref="ITimer"/>
    /// </summary>
    public class Timer : ITimer
    {
        SysTimer _timer;

        /// <summary>
        /// Initializes a new instance of <see cref="Timer"/>
        /// </summary>
        /// <param name="action"><see cref="Action"/> to run on an interval</param>
        /// <param name="interval">The interval in milliseconds</param>
        public Timer(Action action, double interval)
        {
            _timer = new SysTimer(interval)
            {
                Enabled = true,
                AutoReset = true
            };
            _timer.Elapsed += (s,e) => action();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _timer?.Dispose();
            _timer = null;
        }

        /// <inheritdoc/>
        public void Stop()
        {
            _timer.Stop();
        }
    }
}