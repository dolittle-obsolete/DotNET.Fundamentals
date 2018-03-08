using Dolittle.Strings;
using Machine.Specifications;

namespace Dolittle.Specs.Strings.for_StringFormatBuilder.given
{
    public class an_empty_string_format_builder
    {
        protected static StringFormatBuilder builder;

        Establish context = () => builder = new StringFormatBuilder('.');
    }
}
