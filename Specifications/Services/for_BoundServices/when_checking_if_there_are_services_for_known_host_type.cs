/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Logging;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Services.for_BoundServices
{
    public class when_checking_if_there_are_services_for_known_host_type 
    {
        const string host_type = "My Host Type";
        static BoundServices bound_services;
        static bool result;
        Establish context = () => 
        {
            bound_services = new BoundServices(Moq.Mock.Of<ILogger>());
            var service = new Service(ServerServiceDefinition.CreateBuilder().Build(), null);
            bound_services.Register(host_type, new[] { service });
        };

        Because of = () => result = bound_services.HasFor(host_type);

        It should_return_true = () => result.ShouldBeTrue();
    }    

}