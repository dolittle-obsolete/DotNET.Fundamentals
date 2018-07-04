using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.for_application_structure_with_4_correct_segments.given
{
    public class an_application_resource_identifier_converter : all_dependencies
    {
        protected const string artifact_type_name = "TheResourceType";

        protected static ApplicationArtifactIdentifierStringConverter converter;

        protected static Mock<IArtifactTypes> artifact_types;
        protected static Mock<IArtifactType> artifact_type;

        Establish context = () => 
        {
            artifact_types = new Mock<IArtifactTypes>();
            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(a => a.Identifier).Returns(artifact_type_name);

            artifact_types.Setup(a => a.GetFor(artifact_type_name)).Returns(artifact_type.Object);

            converter = new ApplicationArtifactIdentifierStringConverter(application, artifact_types.Object);

        };
    }
}