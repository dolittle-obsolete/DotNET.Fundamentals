using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Resources.for_ResourceDefinitions
{
    public class when_initialized_with_one_resource_definition_in_the_system : given.one_resource_definer
    {
        protected static ResourceDefinitions resource_definitions;

        static Mock<IResourceDefinitionBuilder> builder;
        static Mock<IResourceDefinition> resource_definition;

        Establish context = () =>
        {
            builder = new Mock<IResourceDefinitionBuilder>();
            resource_definition_builder_factory.Setup(b => b.Create()).Returns(builder.Object);
            resource_definition = new Mock<IResourceDefinition>();
            resource_definition.SetupGet(_ => _.Name).Returns("Definition");
            builder.Setup(b => b.Build()).Returns(resource_definition.Object);
        };

        Because of = () => resource_definitions = new ResourceDefinitions(resource_definers_instances.Object, resource_definition_builder_factory.Object);

        It should_call_the_definer = () => resource_definer.Verify(r => r.Define(Moq.It.IsAny<IResourceDefinitionBuilder>()), Times.Once);
        It should_hold_the_built_definition = () => resource_definitions.All.ShouldContainOnly(resource_definition.Object);
    }
}