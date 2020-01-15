// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents a <see cref="INamedPolicy"/>.
    /// </summary>
    public class NamedPolicy : Policy, INamedPolicy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedPolicy"/> class.
        /// </summary>
        /// <param name="name">Name of the policy.</param>
        /// <param name="delegatedPolicy"><see cref="IPolicy"/> to delegate to.</param>
        public NamedPolicy(string name, IPolicy delegatedPolicy)
            : base(delegatedPolicy)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedPolicy"/> class.
        /// </summary>
        /// <param name="name">Name of the policy.</param>
        /// <param name="underlyingPolicy">The underlying <see cref="Polly.Policy"/>.</param>
        public NamedPolicy(string name, Polly.Policy underlyingPolicy)
            : base(underlyingPolicy)
        {
            Name = name;
        }

        /// <inheritdoc/>
        public string Name { get; }
    }
}