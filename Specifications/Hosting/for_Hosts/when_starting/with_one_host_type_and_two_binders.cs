/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Hosting.for_Hosts.when_starting
{
    public class with_one_host_type_and_two_binders : given.one_host_type_with_two_binders
    {
        static Hosts hosts;

        Establish context = () => 
        {
            configuration.Enabled = true;
            hosts = new Hosts(host_types, type_finder.Object, container.Object, bound_services.Object, logger);
        };

        Because of = () => hosts.Start();
        It should_start_host_once = () => host.Verify(_ => _.Start(identifier, configuration, Moq.It.IsAny<IEnumerable<Service>>()), Moq.Times.Once);       
    }    
}