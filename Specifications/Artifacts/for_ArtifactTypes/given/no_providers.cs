using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypes.given
{
    public class no_providers : all_dependencies
    {
        Establish context = () => artifact_types_providers.Setup(_ => _.GetEnumerator()).Returns(new List<ICanProvideArtifactTypes>().GetEnumerator());
    }
}