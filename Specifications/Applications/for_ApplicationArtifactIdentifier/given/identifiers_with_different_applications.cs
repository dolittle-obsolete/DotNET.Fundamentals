using doLittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace doLittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class identifiers_with_different_applications
    {
        protected static ApplicationArtifactIdentifier identifier_a;
        protected static ApplicationArtifactIdentifier identifier_b;

        Establish context = () =>
        {
            var application_a = new Mock<IApplication>();
            application_a.SetupGet(a => a.Name).Returns("ApplicationA");

            var application_b = new Mock<IApplication>();
            application_b.SetupGet(a => a.Name).Returns("ApplicationB");

            var area = (ApplicationArea)"Some Area";
            var location = Mock.Of<IApplicationLocation>();
            var artifact = new Mock<IArtifact>();
            artifact.SetupGet(_ => _.Name).Returns("Artifact");

            identifier_a = new ApplicationArtifactIdentifier(application_a.Object, area, location, artifact.Object);
            identifier_b = new ApplicationArtifactIdentifier(application_b.Object, area, location, artifact.Object);
        };
       
    }
}
