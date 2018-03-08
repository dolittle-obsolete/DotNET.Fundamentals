using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Resources.for_ResourceDefinitions
{
    public class when_initialized_with_two_resource_definers_defining_the_same : given.two_resource_definers_and_definitions
    {
        protected static Exception result;

        Establish context = () => second_resource_definition.SetupGet(_ => _.Name).Returns(() => first_resource_definition_name);

        Because of = () => result = Catch.Exception(() => new ResourceDefinitions(resource_definers_instances.Object, resource_definition_builder_factory.Object));

        It should_throw_multiple_resources_with_same_name = () => result.ShouldBeOfExactType<MultipleResourcesWithSameName>();
    }
}