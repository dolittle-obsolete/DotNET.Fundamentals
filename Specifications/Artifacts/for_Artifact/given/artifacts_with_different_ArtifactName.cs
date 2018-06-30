using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.Specs.for_Artifact.given
{
    public class artifacts_with_different_ArtifactName :two_artifacts
    {
        Establish context = () =>
        {
            var artifactNameA = "ArtifactA";
            var artifactNameB = "ArtifactB";
            var type = new Mock<IArtifactType>();
            type.SetupGet(_ => _.Identifier).Returns("ArtifactType");

            var generation = 1;

            artifactA = new Artifact(artifactNameA, type.Object, generation);
            artifactB = new Artifact(artifactNameB, type.Object, generation);

        };
    }
}