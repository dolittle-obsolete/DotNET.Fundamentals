using Moq;
using Machine.Specifications;
using Dolittle.Types;

namespace Dolittle.Artifacts.for_ArtifactTypes.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<IArtifactType>>   artifact_type_instances;

        Establish context = () => artifact_type_instances = new Mock<IInstancesOf<IArtifactType>>();
    }
}