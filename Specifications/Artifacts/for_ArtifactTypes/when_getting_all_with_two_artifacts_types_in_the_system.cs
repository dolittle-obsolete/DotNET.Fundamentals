using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_all_with_two_artifacts_types_in_the_system : given.two_artifact_types
    {
        static IEnumerable<IArtifactType> result;

        static ArtifactTypes artifact_types;

        Establish context = () => artifact_types = new ArtifactTypes(given.all_dependencies.artifact_type_instances.Object);

        Because of = () => result = artifact_types.All;

        It should_hold_all_four_artifact_types = () => result.ShouldContainOnly(
                                                            first_artifact_type.Object,
                                                            second_artifact_type.Object);
    }
}