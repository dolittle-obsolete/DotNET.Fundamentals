using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{

    public class when_checking_if_identifier_exist_and_it_exists : given.one_artifact_type
    {
        static ArtifactTypes artifact_types;
        static bool result;

        Establish context = ()=> artifact_types = new ArtifactTypes(artifact_type_instances.Object);

        Because of = ()=> result = artifact_types.Exists(identifier);

        It should_exist = ()=> result.ShouldBeTrue();
    }
}