/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Build
{
    /// <summary>
    /// Defines tasks that can gets called after all other tasks are done
    /// </summary>
    public interface ICanPerformPostBuildTask
    {
        /// <summary>
        /// Get the message string to show in output
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Perform method
        /// </summary>
        void Perform();
    }
}