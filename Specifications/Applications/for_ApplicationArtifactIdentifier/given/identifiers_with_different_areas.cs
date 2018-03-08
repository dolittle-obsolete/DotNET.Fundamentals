using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class identifiers_with_different_areas
    {
        protected static ApplicationArtifactIdentifier identifier_a;
        protected static ApplicationArtifactIdentifier identifier_b;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("ApplicationA");

            var area_a = (ApplicationArea)"AreaA";
            var area_b = (ApplicationArea)"AreaB";
            var location = Mock.Of<IApplicationLocation>();
            var artifact = new Mock<IArtifact>();
            artifact.SetupGet(_ => _.Name).Returns("Artifact");

            identifier_a = new ApplicationArtifactIdentifier(application.Object, area_a, location, artifact.Object);
            identifier_b = new ApplicationArtifactIdentifier(application.Object, area_b, location, artifact.Object);
        };
       
    }
}
