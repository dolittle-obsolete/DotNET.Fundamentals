/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
extern alias management;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using grpc = management::Dolittle.DependencyInversion.Management;

namespace Dolittle.DependencyInversion.Management
{
    /// <summary>
    /// Represents all <see cref="Binding">bindings</see> in the system
    /// </summary>
    public class BindingCollection : IEnumerable<grpc.Binding>
    {
        readonly List<grpc.Binding> _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingCollection"/>
        /// </summary>
        /// <param name="bindings">Collection of all <see cref="Binding">bindings</see></param>
        public BindingCollection(IEnumerable<DependencyInversion.Binding> bindings) => _bindings = new List<grpc.Binding>(bindings.Select(_ => _.ToProtobuf()));

        /// <inheritdoc/>
        public IEnumerator<grpc.Binding> GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bindings.GetEnumerator();
        }
    }
}