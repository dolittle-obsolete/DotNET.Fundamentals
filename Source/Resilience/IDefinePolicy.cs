// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining a policy.
    /// </summary>
    public interface IDefinePolicy
    {
        /// <summary>
        /// Define the policy.
        /// </summary>
        /// <returns>The defined <see cref="Polly.Policy"/>.</returns>
        Polly.Policy Define();
    }
}