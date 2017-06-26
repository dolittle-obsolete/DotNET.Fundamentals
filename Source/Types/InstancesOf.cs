/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using doLittle.DependencyInversion;

namespace doLittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IInstancesOf{T}"/>
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface</typeparam>
    public class InstancesOf<T> : IInstancesOf<T>
        where T : class
    {
        IEnumerable<Type> _types;
        IContainer _container;

        /// <summary>
        /// Initalizes an instance of <see cref="IInstancesOf{T}"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> used for finding types</param>
        /// <param name="container"><see cref="IContainer"/> used for managing instances of the types when needed</param>
        public InstancesOf(ITypeFinder typeFinder, IContainer container)
        {
            _types = typeFinder.FindMultiple<T>();
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type) as T;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type);
        }
    }
}
