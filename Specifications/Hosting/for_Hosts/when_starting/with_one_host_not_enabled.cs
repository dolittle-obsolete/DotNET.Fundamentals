/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Hosting.for_Hosts.when_starting
{
    public class with_one_host_not_enabled : given.one_host_type_with_binder
    {
        static Hosts hosts;

        Establish context = () => 
        {
            configuration.Enabled = false;
            hosts = new Hosts(host_types, type_finder.Object, container.Object, bound_services.Object, logger);
        };

        Because of = () => hosts.Start();

        It should_not_bind_services = () => binder.Verify(_ => _.BindServices(), Moq.Times.Never);
        It should_not_pass_services_to_bound_services = () => bound_services.Verify(_ => _.Register(Moq.It.IsAny<HostType>(), Moq.It.IsAny<IEnumerable<ServerServiceDefinition>>()), Moq.Times.Never);
        It should_not_start_host = () => host.Verify(_ => _.Start(identifier, configuration, Moq.It.IsAny<IEnumerable<ServerServiceDefinition>>()), Moq.Times.Never);       
    }
}