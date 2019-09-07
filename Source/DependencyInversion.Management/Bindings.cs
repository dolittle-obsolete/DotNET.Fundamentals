/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GrpcBinding = Dolittle.DependencyInversion.Management.Grpc.Binding;

namespace Dolittle.DependencyInversion.Management
{
    /// <summary>
    /// Represents all <see cref="Binding">bindings</see> in the system
    /// </summary>
    public class BindingCollection : IEnumerable<GrpcBinding>
    {
        readonly List<GrpcBinding> _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingCollection"/>
        /// </summary>
        /// <param name="bindings">Collection of all <see cref="Binding">bindings</see></param>
        public BindingCollection(IEnumerable<Binding> bindings) => _bindings = new List<GrpcBinding>(bindings.Select(_ => _.Convert()));
        
        /// <inheritdoc/>
        public IEnumerator<GrpcBinding> GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }
    }
}