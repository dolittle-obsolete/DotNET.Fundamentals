/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
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
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="BindingCollection"/>
        /// </summary>
        /// <param name="bindings">A <see cref="BindingCollection"/> with all bindings</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ContainerService(BindingCollection bindings, ILogger logger)
        {
            _bindings = bindings;
            _logger = logger;
        }

        /// <inheritdoc/>
        public override Task<Bindings> GetBindings(GetBindingsRequest request, ServerCallContext context)
        {
            _logger.Information("Getting all bindings");

            var bindings = new Bindings();
            bindings.Bindings_.AddRange(_bindings);
            return Task.FromResult(bindings);
        }
    }
}