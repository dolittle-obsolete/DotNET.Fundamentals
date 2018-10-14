/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Resources.Configuration.Specs.given;
using Machine.Specifications;

namespace Dolittle.Resources.Configuration.Specs.for_ResourceConfiguration
{
    public class when_getting_implementation_for_a_service_that_is_not_found : given.a_type_finder_that_finds_a_resource_representation_for_mongo_db_read_models
    {
        static readonly Type service_type = typeof(IEventStore);
        static ResourceConfiguration resource_configuration;

        static Exception result_exception;

        Establish context = () => 
        {
            resource_configuration = new ResourceConfiguration(type_finder_mock.Object);
            resource_configuration.SetResourceType(read_models_resource_type, mongo_db_resource_type_implementation);
        };

        Because of = () => result_exception = Catch.Exception(() => resource_configuration.GetImplementationFor(service_type));

        It should_throw_an_exception = () => result_exception.ShouldNotBeNull();
        It should_throw_ImplementationForServiceNotFound = () => result_exception.ShouldBeOfExactType(typeof(ImplementationForServiceNotFound));

    }
}