/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Build
{
    /// <summary>
    /// Defines a system that is capable of performing all discovered <see cref="ICanPerformPostBuildTask"/>
    /// </summary>
    public interface IPostBuildTaskPerformers
    {
        /// <summary>
        /// Performs all post tasks
        /// </summary>
        void Perform();
    }
}