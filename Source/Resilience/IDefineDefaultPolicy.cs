// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining the default policy for resilience.
    /// </summary>
    public interface IDefineDefaultPolicy
    {
        /// <summary>
        /// Define the default policy.
        /// </summary>
        /// <returns>The defined <see cref="Polly.Policy"/>.</returns>
        Polly.Policy Define();
    }
}