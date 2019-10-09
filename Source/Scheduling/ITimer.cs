/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Scheduling
{
    /// <summary>
    /// Defines an instance of a timer
    /// </summary>
    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Stop a running timer
        /// </summary>
        void Stop();
    }
}