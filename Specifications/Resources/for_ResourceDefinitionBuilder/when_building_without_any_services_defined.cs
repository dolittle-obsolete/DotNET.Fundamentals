using System;
using Machine.Specifications;

namespace Dolittle.Resources.for_ResourceDefinitionBuilder
{
    public class when_building_without_any_services_defined : given.a_resource_definition_builder
    {
        static Exception result;

        Establish context = () => builder.WithName("Something");

        Because of = () => result = Catch.Exception(() => builder.Build());

        It should_throw_no_resource_services_defined = () => result.ShouldBeOfExactType<NoResourceServicesDefined>();
    }
}