using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.Specs.for_Artifact.given
{
    public class one_artifact
    {
        protected static Artifact artifact;

        Establish context = () => 
        {
            var type = new Mock<IArtifactType>();
            type.SetupGet(_ => _.Identifier).Returns("ArtifactType");
            artifact = new Artifact("Artifact", type.Object, 1);
        };
    }
}