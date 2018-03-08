using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Resources.for_ResourceDefinitions
{
    public class when_initialized_with_two_resource_definers_in_the_system : given.two_resource_definers_and_definitions
    {
        protected static ResourceDefinitions resource_definitions;

        Because of = () => resource_definitions = new ResourceDefinitions(resource_definers_instances.Object, resource_definition_builder_factory.Object);

        It should_add_both_definitions = () => resource_definitions.All.ShouldContain(first_resource_definition.Object, second_resource_definition.Object);
    }
}