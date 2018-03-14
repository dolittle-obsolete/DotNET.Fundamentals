using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_for_known_identifier : given.two_artifact_types
    {
        static ArtifactTypes artifact_types;
        static IArtifactType result;

        Establish context = () => artifact_types = new ArtifactTypes(given.all_dependencies.artifact_type_instances.Object);

        Because of = ()=> result = artifact_types.GetFor(second_artifact_type_identifier);

        It should_return_correct_identifier = () => result.ShouldEqual(second_artifact_type.Object);
    }
}