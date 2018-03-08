/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Reflection;
using Dolittle.Execution;
using Dolittle.Reflection;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Represents an implementation of <see cref="IBindingScopeBuilder"/>
    /// </summary>
    public class BindingScopeBuilder : IBindingScopeBuilder
    {
        Binding _binding;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingScopeBuilder"/>
        /// </summary>
        /// <param name="binding"><see cref="Binding"/> to build for</param>
        public BindingScopeBuilder(Binding binding)
        {
            _binding = binding;
        }


        /// <inheritdoc/>
        public void Singleton()
        {
            _binding = new Binding(
                _binding.Service,
                _binding.Strategy,
                new Scopes.Singleton());
        }

        /// <inheritdoc/>
        public Binding Build()
        {
            if( !(_binding.Scope is Scopes.Singleton) && _binding.Strategy.GetTargetType().HasAttribute<SingletonAttribute>() )
                Singleton();

            return _binding;
        }
    }
}