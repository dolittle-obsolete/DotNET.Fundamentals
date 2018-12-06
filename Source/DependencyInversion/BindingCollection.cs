/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Represents an implementation of <see cref="IBindingCollection"/>
    /// </summary>
    public class BindingCollection : IBindingCollection
    {
        readonly List<Binding>  _bindings = new List<Binding>();

        /// <summary>
        /// Initializes a new instance of <see cref="BindingCollection"/>
        /// </summary>
        /// <param name="bindingCollections"></param>
        public BindingCollection(params IEnumerable<Binding>[] bindingCollections)
        {
            bindingCollections.ForEach(_ => 
            {
                var newBindings = _.Where(binding => !HasBindingFor(binding.Service));
                _bindings.AddRange(newBindings);
            });
        }

        /// <inheritdoc/>
        public bool HasBindingFor<T>()
        {
            return _bindings.Any(binding => binding.Service.Equals(typeof(T)));
        }

        /// <inheritdoc/>
        public bool HasBindingFor(Type type)
        {
            return _bindings.Any(binding => binding.Service.Equals(type));
        }

        /// <inheritdoc/>
        public IEnumerator<Binding> GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }


        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }
    }
}