/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion.Strategies
{
    /// <summary>
    /// Represents an <see cref="IActivationStrategy"/> that gets activated through a callback
    /// </summary>
    public class CallbackWithContainer : IActivationStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Callback"/>
        /// </summary>
        /// <param name="target">The callback target</param>
        public CallbackWithContainer(Func<IContainer, object> target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the target
        /// </summary>
        public Func<IContainer, object> Target {  get; }

        /// <inheritdoc/>
        public System.Type GetTargetType()
        {
            return Target.GetType();
        }
    }

    /// <summary>
    /// Represents a generic <see cref="Callback"/>
    /// </summary>
    public class CallbackWithContainer<T> : CallbackWithContainer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Callback"/>
        /// </summary>
        /// <param name="target"></param>
        public CallbackWithContainer(Func<IContainer, T> target) : base((IContainer container) => target(container))
        { }
    }
}