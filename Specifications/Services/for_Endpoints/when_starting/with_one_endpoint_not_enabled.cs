/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Services.for_Endpoints.when_starting
{
    public class with_one_host_not_enabled : given.one_service_type_with_binder
    {
        static Endpoints endpoints;

        Establish context = () => 
        {
            configuration.Enabled = false;
            endpoints = new Endpoints(service_types, endpoints_configuration, type_finder.Object, container.Object, bound_services.Object, logger);
        };

        Because of = () => endpoints.Start();

        It should_not_bind_services = () => binder.Verify(_ => _.BindServices(), Moq.Times.Never);
        It should_not_pass_services_to_bound_services = () => bound_services.Verify(_ => _.Register(Moq.It.IsAny<ServiceType>(), Moq.It.IsAny<IEnumerable<Service>>()), Moq.Times.Never);
        It should_not_start_endpoint = () => endpoint.Verify(_ => _.Start(EndpointVisibility.Public, configuration, Moq.It.IsAny<IEnumerable<Service>>()), Moq.Times.Never);       
    }
}