/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IImplementationsOf{T}"/>
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface</typeparam>
    public class ImplementationsOf<T> : IImplementationsOf<T>
        where T : class
    {
        IEnumerable<Type> _types;

        /// <summary>
        /// Initializes a new instance of <see cref="ImplementationsOf{T}"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding types</param>
        public ImplementationsOf(ITypeFinder typeFinder)
        {
            _types = typeFinder.FindMultiple<T>();
        }

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _types.GetEnumerator();
        }
    }
}