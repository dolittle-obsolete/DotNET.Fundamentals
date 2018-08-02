using Machine.Specifications;

namespace Dolittle.Applications.for_Version
{
    public class when_parsing_with_pre_release_tag
    {
        static Version result;

        Because of = () => result = Version.FromString("1.2.3-alpha2.4");

        It should_hold_correct_major = () => result.Major.ShouldEqual(1);
        It should_hold_correct_minor = () => result.Minor.ShouldEqual(2);
        It should_hold_correct_patch = () => result.Patch.ShouldEqual(3);
        It should_hold_correct_build = () => result.Build.ShouldEqual(4);
        It should_hold_correct_pre_release_tag = () => result.PreReleaseTag.ShouldEqual("alpha2");
    }
}