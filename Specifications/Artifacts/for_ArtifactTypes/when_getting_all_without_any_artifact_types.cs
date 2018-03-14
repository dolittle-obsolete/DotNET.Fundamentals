using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_all_without_any_artifact_types : given.no_artifact_types
    {
        static ArtifactTypes artifact_types;
        static IEnumerable<IArtifactType> result;

        Establish context = () => artifact_types = new ArtifactTypes(artifact_type_instances.Object);

        Because of = () => result = artifact_types.All;

        It should_return_no_providers = () => result.ShouldBeEmpty();
    }
}