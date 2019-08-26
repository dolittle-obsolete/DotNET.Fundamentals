/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents a <see cref="IPolicyFor{T}"/>
    /// </summary>
    public class PolicyFor<T> : Policy, IPolicyFor<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NamedPolicy"/>
        /// </summary>
        /// <param name="delegatedPolicy"><see cref="IPolicy"/> to delegate to</param>
        public PolicyFor(IPolicy delegatedPolicy) : base(delegatedPolicy)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NamedPolicy"/>
        /// </summary>
        /// <param name="underlyingPolicy">The underlying <see cref="Polly.Policy"/></param>
        public PolicyFor(Polly.Policy underlyingPolicy) : base(underlyingPolicy)
        {
        }
    }
}