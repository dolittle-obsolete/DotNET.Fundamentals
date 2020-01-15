// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias management;

using System.Threading.Tasks;
using Dolittle.Logging;
using Grpc.Core;
using management::Dolittle.DependencyInversion.Management;
using static management::Dolittle.DependencyInversion.Management.Container;
using grpc = management::Dolittle.DependencyInversion.Management;

namespace Dolittle.DependencyInversion.Management
{
    /// <summary>
    /// Represents the implementation of the <see cref="ContainerBase"/> service.
    /// </summary>
    public class ContainerService : ContainerBase
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerService"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ContainerService(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override Task<Bindings> GetBindings(GetBindingsRequest request, ServerCallContext context)
        {
            _logger.Information("Getting all bindings");

            var bindings = new grpc.Bindings();
            bindings.Bindings_.AddRange(PostContainerBootStage.AllBindings);
            return Task.FromResult(bindings);
        }
    }
}