using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_checking_if_identifier_exist_without_it_existing : given.no_artifact_types
    {
        static ArtifactTypes artifact_types;
        static bool result;

        Establish context = () => artifact_types = new ArtifactTypes(given.all_dependencies.artifact_type_instances.Object);

        Because of = () => result = artifact_types.Exists("Fourty Two");

        It should_not_exist = () => result.ShouldBeFalse();
    }
}