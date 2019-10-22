/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Logging;
using Grpc.Core;
using Machine.Specifications;

namespace Dolittle.Services.for_BoundServices
{
    public class when_registering
    {
        const string service_type = "My Service Type";

        static BoundServices bound_services;
        static Service first_service;
        static Service second_service;

        Establish context = () =>
        {
            bound_services = new BoundServices(Moq.Mock.Of<ILogger>());

            first_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            second_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
        };

        Because of = () => bound_services.Register(service_type, new [] { first_service, second_service });

        It should_hold_the_registered_services = () => bound_services.GetFor(service_type).ShouldContainOnly(new [] {  first_service, second_service });
    }

}