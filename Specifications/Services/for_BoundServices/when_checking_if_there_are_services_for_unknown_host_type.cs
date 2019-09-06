/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Services.for_BoundServices
{
    public class when_checking_if_there_are_services_for_unknown_host_type 
    {
        const string host_type = "My Host Type";
        static BoundServices bound_services;
        static bool result;
        Establish context = () => bound_services = new BoundServices(Moq.Mock.Of<ILogger>());

        Because of = () => result = bound_services.HasFor(host_type);

        It should_return_false = () => result.ShouldBeFalse();
    }    

}