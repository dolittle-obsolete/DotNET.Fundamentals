/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a named policy
    /// </summary>
    public interface INamedPolicy : IPolicy
    {
        /// <summary>
        /// Gets the name of the policy
        /// </summary>
        string Name { get; }
    }
}