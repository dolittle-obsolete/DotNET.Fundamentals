using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;
        protected static Mock<IArtifactTypes> artifact_types;

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            artifact_types = new Mock<IArtifactTypes>();
        };
    }
}
