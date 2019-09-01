/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Grpc.Core;
using static Dolittle.DependencyInversion.Grpc.Container;

namespace Dolittle.DependencyInversion.Grpc
{
    /// <summary>
    /// Represents the implementation of the <see cref="ContainerBase"/> service
    /// </summary>
    public class ContainerService : ContainerBase
    {
        readonly BindingCollection _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingCollection"/>
        /// </summary>
        /// <param name="bindings">A <see cref="BindingCollection"/> with all bindings</param>
        public ContainerService(BindingCollection bindings)
        {
            _bindings = bindings;
        }

        /// <inheritdoc/>
        public override Task<Bindings> GetBindings(GetBindingsRequest request, ServerCallContext context)
        {
            var bindings = new Bindings();
            bindings.Bindings_.AddRange(_bindings);
            return Task.FromResult(bindings);
        }
    }
}