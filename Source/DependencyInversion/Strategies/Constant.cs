/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.DependencyInversion.Strategies
{
    /// <summary>
    /// Represents an <see cref="IActivationStrategy"/> that gets activated with a constant
    /// </summary>
    public class Constant : IActivationStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Callback"/>
        /// </summary>
        /// <param name="target">Target constant</param>
        public Constant(object target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the constant target
        /// </summary>
        public object Target { get; }

        /// <inheritdoc/>
        public System.Type GetTargetType()
        {
            return Target.GetType();
        }
    }    
}