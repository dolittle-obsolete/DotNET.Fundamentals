/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Defines a builder of <see cref="Binding"/>
    /// </summary>
    public interface IBindingBuilder
    {
        /// <summary>
        /// Bind to a type
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To<T>();

        /// <summary>
        /// Bind to a type
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(Type type);

        /// <summary>
        /// Bind to a constant
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(object constant);

        /// <summary>
        /// Bind to a callback
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(Func<object> callback);

        /// <summary>
        /// Bind to a callback that expects an instance of the <see cref="IContainer"/>
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(Func<IContainer, object> callbackWithContainer);

        /// <summary>
        /// Builds the Binding
        /// </summary>
        /// <returns>The resulting <see cref="Binding"/></returns>
        Binding Build();
    }

    /// <summary>
    /// Defines a typed builder of <see cref="Binding"/>
    /// </summary>
    public interface IBindingBuilder<T> : IBindingBuilder
    {
        /// <summary>
        /// Bind to a type
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        new IBindingScopeBuilder To<TTarget>() where TTarget:T;


        /// <summary>
        /// Bind to a constant
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(T constant);
        
        /// <summary>
        /// Bind to a type
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(Func<T> callback);

        /// <summary>
        /// Bind to a type that expects an instance of the <see cref="IContainer"/>
        /// </summary>
        /// <returns><see cref="IBindingScopeBuilder"/> for building scope</returns>
        IBindingScopeBuilder To(Func<IContainer, T> callbackWithContainer);
    }
}