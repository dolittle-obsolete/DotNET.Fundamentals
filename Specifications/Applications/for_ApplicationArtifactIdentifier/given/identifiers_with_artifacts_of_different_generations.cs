using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class identifiers_with_artifacts_of_different_generations
    {
        protected static ApplicationArtifactIdentifier identifier_a;
        protected static ApplicationArtifactIdentifier identifier_b;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("SomeApplication");
            var area = (ApplicationArea)"Some Area";
            var location = Mock.Of<IApplicationLocation>(_ => _.Equals(
                Moq.It.IsAny<IApplicationLocation>()) == true
                );
            
            var artifactType = new Mock<IArtifactType>();
            artifactType.SetupGet(_ => _.Identifier).Returns("Command");
            
            var artifactA = new Mock<IArtifact>();
            artifactA.SetupGet(_ => _.Name).Returns("Artifact");
            artifactA.SetupGet(_ => _.Generation).Returns(1);
            artifactA.SetupGet(_ => _.Type).Returns(artifactType.Object);
            
            var artifactB = new Mock<IArtifact>();
            artifactA.SetupGet(_ => _.Name).Returns("Artifact");
            artifactA.SetupGet(_ => _.Generation).Returns(2);
            artifactA.SetupGet(_ => _.Type).Returns(artifactType.Object);

            identifier_a = new ApplicationArtifactIdentifier(application.Object, area, location, artifactA.Object);
            identifier_b = new ApplicationArtifactIdentifier(application.Object, area, location, artifactB.Object);
        };
    }
}