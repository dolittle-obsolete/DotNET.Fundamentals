/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining a resilience policy for a specific type
    /// </summary>
    public interface ICanDefinePolicyFor<T> : ICanDefinePolicy
    {

    }
}