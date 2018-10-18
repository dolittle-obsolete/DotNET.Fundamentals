/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Resources.Configuration.Specs.given;
using Machine.Specifications;

namespace Dolittle.Resources.Configuration.Specs.for_ResourceConfiguration
{
    public class when_setting_resource_type_that_is_already_set_with_another_implementation : given.a_type_finder_that_finds_a_resource_type_with_first_service_for_first_resource_type_and_first_implementation
    {
        static readonly Type service_type = typeof(second_service);
        static ResourceConfiguration resource_configuration;

        static Exception result_exception;

        Establish context = () => 
        {
            resource_configuration = new ResourceConfiguration(type_finder_mock.Object);
            resource_configuration.SetResourceType(first_resource_type, first_resource_type_implementation);
        };

        Because of = () => result_exception = Catch.Exception(() => resource_configuration.SetResourceType(first_resource_type, second_resource_type_implementation));

        It should_throw_an_exception = () => result_exception.ShouldNotBeNull();
        It should_throw_ResourceTypeAlreadySet = () => result_exception.ShouldBeOfExactType(typeof(ResourceTypeAlreadySet));
    }
}