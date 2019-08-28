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
    public class CallbackWithBindingContext : IActivationStrategy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Callback"/>
        /// </summary>
        /// <param name="target">The callback target</param>
        public CallbackWithBindingContext(Func<BindingContext,object> target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the target
        /// </summary>
        public Func<BindingContext, object> Target {  get; }

        /// <inheritdoc/>
        public System.Type GetTargetType()
        {
            return Target.GetType();
        }
    }

    /// <summary>
    /// Represents a generic <see cref="Callback"/>
    /// </summary>
    public class CallbackWithBindingContext<T> : CallbackWithBindingContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Callback"/>
        /// </summary>
        /// <param name="target"></param>
        public CallbackWithBindingContext(Func<BindingContext, T> target) : base((bindingContext) => target(bindingContext))
        { }
    }
}