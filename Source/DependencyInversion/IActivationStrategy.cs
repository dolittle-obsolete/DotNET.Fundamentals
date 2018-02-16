/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.DependencyInversion
{
    /// <summary>
    /// Defines the strategy in which a binding will be provided
    /// </summary>
    public interface IActivationStrategy
    {
        /// <summary>
        /// Get the target type
        /// </summary>
        /// <returns>The type of the target - typically the implementing type</returns>
        Type GetTargetType();
    }
}