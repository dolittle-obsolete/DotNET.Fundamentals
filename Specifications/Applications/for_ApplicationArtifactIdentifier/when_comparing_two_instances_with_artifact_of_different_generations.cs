
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier
{
    public class when_comparing_two_instances_with_artifact_of_different_generations 
        : given.identifiers_with_artifacts_of_different_generations
    {
        static bool result;

        Because of = () => result = identifier_a == identifier_b;

        It should_not_be_considered_the_same = () => result.ShouldBeFalse();
    }
}