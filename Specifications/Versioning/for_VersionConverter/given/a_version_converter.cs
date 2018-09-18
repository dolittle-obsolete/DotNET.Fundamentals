using Machine.Specifications;

namespace Dolittle.Versioning.for_VersionConverter.given
{
    public class a_version_converter
    {
        protected static VersionConverter version_converter;

        Establish context = () => version_converter = new VersionConverter();
    }
}