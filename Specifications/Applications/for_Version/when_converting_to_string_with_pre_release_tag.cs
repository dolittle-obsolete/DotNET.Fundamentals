using Machine.Specifications;

namespace Dolittle.Applications.for_Version
{
    public class when_converting_to_string_with_pre_release_tag 
    {
        static Version version;
        static string result;

        Establish context = () => version = new Version(1,2,3,4, "alpha2");

        Because of = () => result = version.ToString();

        It should_format_correctly = () => result.ShouldEqual("1.2.3-alpha2.4");
    }
}