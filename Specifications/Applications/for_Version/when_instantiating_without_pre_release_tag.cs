using Machine.Specifications;

namespace Dolittle.Applications.for_Version
{
    public class when_instantiating_without_pre_release_tag
    {
        static Version result;
        Because of = () => result = new Version(1,2,3,4);
        It should_not_be_considered_a_pre_release = () => result.IsPreRelease.ShouldBeFalse();
    }
}