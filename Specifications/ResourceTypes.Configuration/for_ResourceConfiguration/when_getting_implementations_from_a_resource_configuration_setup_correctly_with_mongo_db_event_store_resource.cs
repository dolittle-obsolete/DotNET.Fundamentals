/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Logging;
using Dolittle.ResourceTypes.Configuration.Specs.given;
using Machine.Specifications;

namespace Dolittle.ResourceTypes.Configuration.Specs.for_ResourceConfiguration
{
    public class when_getting_implementations_from_a_resource_configuration_setup_correctly_with_mongo_db_event_store_resource : given.a_type_finder_that_finds_a_resource_type_with_second_and_third_service_for_second_resource_type_and_first_implementation
    {
        static readonly Type first_service_type = typeof(second_service);
        static readonly Type first_service_type_correct_implementation_type = typeof(implementation_of_second_service_for_first_implementation_type);
        static readonly Type second_service_type = typeof(third_service);
        static readonly Type second_service_type_correct_implementation_type = typeof(Geodesics);
        static ResourceConfiguration resource_configuration;

        static Type first_result_implementation_type;
        static Type second_result_implementation_type;

        Establish context = () => 
        {
            resource_configuration = new ResourceConfiguration(type_finder_mock.Object, Moq.Mock.Of<ILogger>());
            resource_configuration.ConfigureResourceTypes(new Dictionary<ResourceType, ResourceTypeImplementation>{{second_resource_type, first_resource_type_implementation}});
        };

        Because of = () => 
        {
            first_result_implementation_type = resource_configuration.GetImplementationFor(first_service_type);

            second_result_implementation_type = resource_configuration.GetImplementationFor(second_service_type);
        };

        It should_return_the_first_implementation_type = () => first_result_implementation_type.ShouldNotBeNull();
        It should_return_the_second_implementation_type = () => second_result_implementation_type.ShouldNotBeNull();
        It should_return_the_first_correct_implementation_type = () => first_result_implementation_type.ShouldEqual(first_service_type_correct_implementation_type);
        It should_return_the_second_correct_implementation_type = () => second_result_implementation_type.ShouldEqual(second_service_type_correct_implementation_type);        
    }
}