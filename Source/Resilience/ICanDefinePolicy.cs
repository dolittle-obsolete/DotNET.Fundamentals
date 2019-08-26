/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Polly;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining a policy
    /// </summary>
    public interface ICanDefinePolicy
    {
        /// <summary>
        /// Define the policy
        /// </summary>
        Polly.Policy Define();
    }
}