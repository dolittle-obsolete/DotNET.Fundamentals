using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_for_known_identifier : given.two_providers
    {
        const string identifier = "Fourty Two";
        static ArtifactTypes artifact_types;

        static Mock<IArtifactType>  artifact_type;
        static IArtifactType result;

        Establish context = () => 
        {   
            artifact_type = new Mock<IArtifactType>();
            artifact_type.Setup(_ => _.Identifier).Returns(identifier);
            first_provider_artifact_types.Add(artifact_type.Object);
            var second_artifact_type = new Mock<IArtifactType>();
            second_artifact_type.Setup(_ => _.Identifier).Returns("Other Thing");
            first_provider_artifact_types.Add(second_artifact_type.Object);

            artifact_types = new ArtifactTypes(artifact_types_providers.Object);
        };

        Because of = ()=> result = artifact_types.GetFor(identifier);

        It should_return_correct_identifier = () => result.ShouldEqual(artifact_type.Object);
    }
}