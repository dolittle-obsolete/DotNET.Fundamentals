using Machine.Specifications;
using Moq;

namespace Dolittle.Resources.for_ResourceDefinitions.given
{

    public class two_resource_definers_and_definitions : two_resource_definers
    {
        protected static Mock<IResourceDefinitionBuilder> first_builder;
        protected static Mock<IResourceDefinitionBuilder> second_builder;
        protected static Mock<IResourceDefinition> first_resource_definition;
        protected static Mock<IResourceDefinition> second_resource_definition;

        protected static string first_resource_definition_name = "First";
        protected static string second_resource_definition_name = "Second";

        Establish context = () =>
        { 
            first_builder = new Mock<IResourceDefinitionBuilder>();
            second_builder = new Mock<IResourceDefinitionBuilder>();
            resource_definition_builder_factory.SetupSequence(_ => _.Create())
                .Returns(first_builder.Object)
                .Returns(second_builder.Object);
                
            first_resource_definition = new Mock<IResourceDefinition>();
            first_resource_definition.SetupGet(_ => _.Name).Returns(() => first_resource_definition_name);
            second_resource_definition = new Mock<IResourceDefinition>();
            second_resource_definition.SetupGet(_ => _.Name).Returns(() => second_resource_definition_name);

            first_builder.Setup(b => b.Build()).Returns(first_resource_definition.Object);
            second_builder.Setup(b => b.Build()).Returns(second_resource_definition.Object);
        };
    }
}