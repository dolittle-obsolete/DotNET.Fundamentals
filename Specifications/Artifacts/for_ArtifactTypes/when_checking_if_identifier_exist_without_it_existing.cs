using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_checking_if_identifier_exist_without_it_existing : given.no_providers
    {
        static ArtifactTypes artifact_types;
        static bool result;

        Establish context = () => artifact_types = new ArtifactTypes(artifact_types_providers.Object);

        Because of = () => result = artifact_types.Exists("Fourty Two");

        It should_not_exist = () => result.ShouldBeFalse();
    }
}