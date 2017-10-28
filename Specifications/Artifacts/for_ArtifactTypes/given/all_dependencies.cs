using Moq;
using Machine.Specifications;
using doLittle.Types;

namespace doLittle.Artifacts.for_ArtifactTypes.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanProvideArtifactTypes>>   artifact_types_providers;

        Establish context = () => artifact_types_providers = new Mock<IInstancesOf<ICanProvideArtifactTypes>>();
    }
}