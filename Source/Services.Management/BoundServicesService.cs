/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpc = Dolittle.Management.Services;
using static Dolittle.Management.Services.BoundServices;

namespace Dolittle.Services.Management
{
    /// <summary>
    /// Represents an implementation of <see cref="BoundServicesBase"/> for working with <see cref="Service">services</see>
    /// that are bound in our system
    /// </summary>
    public class BoundServicesService : BoundServicesBase
    {
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundServicesService"/>
        /// </summary>
        /// <param name="boundServices">Underlying <see cref="IBoundServices"/></param>
        public BoundServicesService(IBoundServices boundServices)
        {
            _boundServices = boundServices;
        }

        /// <inheritdoc/>
        public override Task<grpc.Services> GetForServiceType(grpc.ServiceType request, ServerCallContext context)
        {
            var boundServices = _boundServices.GetFor(request.Name);
            var services = new grpc.Services();
            services.BoundServices.Add(boundServices.Select(_ => new grpc.Service { Name = _.Descriptor.FullName }));
            return Task.FromResult(services);
        }
    }
}