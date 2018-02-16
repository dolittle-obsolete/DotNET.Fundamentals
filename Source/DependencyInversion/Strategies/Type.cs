/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace doLittle.DependencyInversion.Strategies
{
    /// <summary>
    /// Represents an <see cref="IActivationStrategy"/> that has a Type as its target
    /// </summary>
    public class Type : IActivationStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Type"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> representing the target</param>
        public Type(System.Type type)
        {
            Target = type;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> target
        /// </summary>
        public System.Type Target { get; }

        /// <inheritdoc/>
        public System.Type GetTargetType()
        {
            return Target;
        }
    }
}