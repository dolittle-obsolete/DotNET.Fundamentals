using System;
using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_ValueConverters
{
    public class when_getting_value_converter_with_no_converter_able : given.no_value_converters
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => value_converters.GetConverterFor(typeof(string)));

        It should_throw_missing_value_converter = () => result.ShouldBeOfExactType<MissingValueConverter>();
    }
}