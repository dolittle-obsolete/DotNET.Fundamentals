using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier
{
    public class when_comparing_two_instances_representing_same_artifact : given.same_artifacts
    {
        static bool result;

        Because of = () => result = identifier_a == identifier_b;

        It should_be_considered_the_same = () => result.ShouldBeTrue();
    }
}
