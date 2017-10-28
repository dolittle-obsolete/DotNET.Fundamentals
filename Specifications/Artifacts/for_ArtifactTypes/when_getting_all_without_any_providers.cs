using System.Collections.Generic;
using Machine.Specifications;

namespace doLittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_all_without_any_providers : given.no_providers
    {
        static ArtifactTypes artifact_types;
        static IEnumerable<IArtifactType> result;

        Establish context = () => artifact_types = new ArtifactTypes(artifact_types_providers.Object);

        Because of = () => result = artifact_types.All;

        It should_return_no_providers = () => result.ShouldBeEmpty();
    }
}