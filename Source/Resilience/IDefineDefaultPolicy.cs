/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines a system that is capable of defining the default policy for resilience
    /// </summary>
    public interface IDefineDefaultPolicy
    {
        /// <summary>
        /// Define the default policy
        /// </summary>
        Polly.Policy Define();
    }
}