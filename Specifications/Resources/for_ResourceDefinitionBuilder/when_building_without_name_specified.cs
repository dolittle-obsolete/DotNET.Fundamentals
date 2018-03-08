using System;
using Machine.Specifications;

namespace Dolittle.Resources.for_ResourceDefinitionBuilder
{

    public class when_building_without_name_specified : given.a_resource_definition_builder
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => builder.Build());

        It should_throw_missing_name_from_resource_definition = () => result.ShouldBeOfExactType<MissingNameFromResourceDefinition>();
    }
}