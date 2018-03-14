using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypes.given
{
    public class no_artifact_types : all_dependencies
    {
        Establish context = () => artifact_type_instances.Setup(_ => _.GetEnumerator()).Returns(new List<IArtifactType>().GetEnumerator());
    }
}