/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining a named resilience policy  
    /// </summary>
    public interface ICanDefineNamedPolicy
    {
        /// <summary>
        /// Gets the name of the policy
        /// </summary>
        string Name { get; }
    }
}