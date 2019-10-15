/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Management;

namespace Dolittle.Services.Management
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanBindManagementServices"/> for exposing
    /// management service implementations for DependencyInversion
    /// </summary>
    public class ManagementServices : ICanBindManagementServices
    {
        readonly BoundServicesService _boundServicesService;

        /// <summary>
        /// Initializes a new instance of <see cref="ManagementServices"/>
        /// </summary>
        /// <param name="boundServicesService">The <see cref="BoundServicesService"/></param>
        public ManagementServices(BoundServicesService boundServicesService)
        {
            _boundServicesService = boundServicesService;
        }

        /// <inheritdoc/>
        public IEnumerable<Dolittle.Services.Service> BindServices()
        {
            return new [] {
                new Dolittle.Services.Service(_boundServicesService, BoundServices.BindService(_boundServicesService), BoundServices.Descriptor)
            };
        }
    }
}