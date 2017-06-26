/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;

namespace doLittle.DependencyInversion.Conventions
{
    /// <summary>
    /// Defines a base abstract class for Binding conventions for any <see cref="IContainer"/>
    /// </summary>
    public abstract class BindingConvention : IBindingConvention
    {
        /// <summary>
        /// Gets or sets the <see cref="BindingLifecycle">ActivationScope</see> that will be used as default
        /// </summary>
        public BindingLifecycle DefaultScope { get; set; }

        /// <inheritdoc/>
        public abstract bool CanResolve(IContainer container, Type service);

        /// <inheritdoc/>
        public abstract void Resolve(IContainer container, Type service);

        /// <summary>
        /// Handle scope for a target type
        /// </summary>
        /// <param name="targetType">Target type</param>
        /// <returns><see cref="BindingLifecycle"/> for the target type</returns>
        /// <remarks>
        /// If the target is marked with the <see cref="SingletonAttribute">Singleton</see> attribute, it will use
        /// that scope instead, as that is a explicit implementation information.
        /// 
        /// Otherwise it will use the DefaultScope
        /// </remarks>
        protected BindingLifecycle GetScopeForTarget(Type targetType)
        {
            var attributes = targetType.GetTypeInfo().GetCustomAttributes(typeof(SingletonAttribute), false).ToArray();
            return attributes.Length == 1 ? BindingLifecycle.Singleton : DefaultScope;
        }
    }
}