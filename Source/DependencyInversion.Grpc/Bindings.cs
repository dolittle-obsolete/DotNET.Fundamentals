/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.DependencyInversion.Grpc
{
    /// <summary>
    /// Represents all <see cref="Binding">bindings</see> in the system
    /// </summary>
    public class BindingCollection : IEnumerable<Binding>
    {
        readonly List<Binding> _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="bindings">Collection of all <see cref="Binding">bindings</see></param>
        public BindingCollection(IEnumerable<DependencyInversion.Binding> bindings) => _bindings = new List<Binding>(bindings.Select(_ => _.Convert()));
        
        /// <inheritdoc/>
        public IEnumerator<Binding> GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }
    }
}