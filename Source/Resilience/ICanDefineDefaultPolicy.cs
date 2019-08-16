/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Polly;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents a system that is capable of defining the default policy for resilience
    /// </summary>
    public interface ICanDefineDefaultPolicy
    {
        /// <summary>
        /// Define the default policy
        /// </summary>
        Policy Define();
    }
}