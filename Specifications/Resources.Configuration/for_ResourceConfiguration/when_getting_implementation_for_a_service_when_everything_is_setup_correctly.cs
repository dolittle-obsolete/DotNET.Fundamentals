/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Resources.Configuration.Specs.given;
using Machine.Specifications;

namespace Dolittle.Resources.Configuration.Specs.for_ResourceConfiguration
{
    public class when_getting_implementation_for_a_service_when_everything_is_setup_correctly : given.a_type_finder_that_finds_a_resource_type_with_first_service_for_first_resource_type_and_first_implementation
    {
        static readonly Type service_type = typeof(first_service);
        static readonly Type service_type_correct_implementation_type = typeof(implementation_of_first_service_for_first_implementation_type);
        static ResourceConfiguration resource_configuration;

        static Type result_implementation_type;

        Establish context = () => 
        {
            resource_configuration = new ResourceConfiguration(type_finder_mock.Object);
            resource_configuration.ConfigureResourceTypes(new Dictionary<ResourceType, ResourceTypeImplementation>{{first_resource_type, first_resource_type_implementation}});
        };

        Because of = () => result_implementation_type = resource_configuration.GetImplementationFor(service_type);

        It should_return_an_implementation_type = () => result_implementation_type.ShouldNotBeNull();
        It should_return_the_correct_implementation_type = () => result_implementation_type.ShouldEqual(service_type_correct_implementation_type);

    }
}