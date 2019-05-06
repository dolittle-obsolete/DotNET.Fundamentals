/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Represents an implementation of <see cref="IBindingBuilder"/>
    /// </summary>
    public class BindingBuilder : IBindingBuilder
    {
        /// <summary>
        /// The binding we're currently building
        /// </summary>
        protected Binding _binding;

        /// <summary>
        /// The scope builder we're currently building with
        /// </summary>
        protected IBindingScopeBuilder _scopeBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingBuilder"/>
        /// </summary>
        /// <param name="binding"><see cref="Binding"/> to build from</param>
        public BindingBuilder(Binding binding)
        {
            _binding = binding;
            _scopeBuilder = new BindingScopeBuilder(binding);
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To<T>()
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Type(typeof(T)),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To(Type type)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Type(type),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To(object constant)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Constant(constant),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To(Func<object> callback)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Callback(callback),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }
        /// <inheritdoc/>
        public IBindingScopeBuilder To(Func<Type> callback)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.TypeCallback(callback),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public Binding Build()
        {
            _binding = _scopeBuilder.Build();
            return _binding;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BindingBuilder<T> : BindingBuilder, IBindingBuilder<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public BindingBuilder(Binding binding) : base(binding) { }

        /// <inheritdoc/>
        IBindingScopeBuilder IBindingBuilder<T>.To<TTarget>()
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Type(typeof(TTarget)),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To(T constant)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Constant(constant),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }

        /// <inheritdoc/>
        public IBindingScopeBuilder To(Func<T> callback)
        {
            _binding = new Binding(
                _binding.Service,
                new Strategies.Callback<T>(callback),
                _binding.Scope);

            _scopeBuilder = new BindingScopeBuilder(_binding);
            return _scopeBuilder;
        }
    }
}