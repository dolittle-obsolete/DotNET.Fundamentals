/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Services.for_Endpoints.when_starting
{
    public class with_one_host_enabled : given.one_service_type_with_binder
    {
        static Endpoints endpoints;

        Establish context = () => 
        {
            configuration.Enabled = true;
            endpoints = new Endpoints(service_types, endpoints_configuration, type_finder.Object, container.Object, bound_services.Object, logger);
        };

        Because of = () => endpoints.Start();

        It should_bind_services = () => binder.Verify(_ => _.BindServices(), Moq.Times.Once);
        It should_pass_services_to_bound_services = () => bound_services.Verify(_ => _.Register(service_type_identifier, Moq.It.IsAny<IEnumerable<Service>>()), Moq.Times.Once);
        It should_start_endpoint = () => endpoint.Verify(_ => _.Start(EndpointType.Public, configuration, Moq.It.IsAny<IEnumerable<Service>>()), Moq.Times.Once);       
    }    
}