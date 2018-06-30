using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.Specs.for_Artifact.given
{
    public class same_artifacts : two_artifacts
    {
        Establish context = () => 
        {
            var type = new Mock<IArtifactType>();
            type.SetupGet(_ => _.Identifier).Returns("ArtifactType");
            artifactA = new Artifact("Artifact", type.Object, 1);
            artifactB = new Artifact("Artifact", type.Object, 1);
        };
    }
}