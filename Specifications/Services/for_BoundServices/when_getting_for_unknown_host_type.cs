/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Services.for_BoundServices
{
    public class when_getting_for_unknown_host_type 
    {
        const string host_type = "My Host Type";
        static BoundServices bound_services;
        static Exception result;
        Establish context = () => bound_services = new BoundServices(Moq.Mock.Of<ILogger>());

        Because of = () => result = Catch.Exception(() => bound_services.GetFor(host_type));

        It should_throw_unknown_host_type = () => result.ShouldBeOfExactType<UnknownHostType>();
    }

}