/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace doLittle.DependencyInversion
{
    /// <summary>
    /// Represents an implementation of <see cref="IBindingProviderBuilder"/>
    /// </summary>
    public class BindingProviderBuilder : IBindingProviderBuilder
    {
        readonly List<Binding>    _bindings = new List<Binding>();

        /// <inheritdoc/>
        public IBindingBuilder<T> Bind<T>()
        {
            var binding = new Binding(typeof(T),new Strategies.Null(), new Scopes.Transient());
            _bindings.Add(binding);
            return new BindingBuilder<T>(binding);
        }

        /// <inheritdoc/>
        public IBindingBuilder Bind(Type type)
        {
            var binding = new Binding(type,new Strategies.Null(), new Scopes.Transient());
            _bindings.Add(binding);
            return new BindingBuilder(binding);
        }

        /// <inheritdoc/>
        public IBindingCollection Build()
        {
            return new BindingCollection(_bindings);
        }
    }
}