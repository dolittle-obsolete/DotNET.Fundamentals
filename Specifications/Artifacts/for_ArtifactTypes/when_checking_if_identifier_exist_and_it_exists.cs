using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{

    public class when_checking_if_identifier_exist_and_it_exists : given.two_providers
    {
        const string identifier = "Fourty Two";
        static ArtifactTypes artifact_types;

        static Mock<IArtifactType>  artifact_type;
        static bool result;

        Establish context = () => 
        {   
            artifact_type = new Mock<IArtifactType>();
            artifact_type.Setup(_ => _.Identifier).Returns(identifier);
            first_provider_artifact_types.Add(artifact_type.Object);
            artifact_types = new ArtifactTypes(artifact_types_providers.Object);
        };

        Because of = ()=> result = artifact_types.Exists(identifier);

        It should_exist = () => result.ShouldBeTrue();
    }
}