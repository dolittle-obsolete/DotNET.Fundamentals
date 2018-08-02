using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_Version
{
    public class when_parsing_invalid_string
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => Version.FromString("1.2.3.alpha2.4"));

        It should_throw_invalid_version_string = () => result.ShouldBeOfExactType<InvalidVersionString>();
    }
}