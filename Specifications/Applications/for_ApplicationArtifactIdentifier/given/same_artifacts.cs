using doLittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace doLittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class same_artifacts
    {
        protected static ApplicationArtifactIdentifier identifier_a;
        protected static ApplicationArtifactIdentifier identifier_b;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("SomeApplication");
            var area = (ApplicationArea)"Some Area";
            var location = new Mock<IApplicationLocation>();
            location.Setup(_ => _.Equals(Moq.It.IsAny<IApplicationLocation>())).Returns(true);
            var artifact = new Mock<IArtifact>();
            artifact.SetupGet(_ => _.Name).Returns("Artifact");

            identifier_a = new ApplicationArtifactIdentifier(application.Object, area, location.Object, artifact.Object);
            identifier_b = new ApplicationArtifactIdentifier(application.Object, area, location.Object, artifact.Object);
        };
    }
}
