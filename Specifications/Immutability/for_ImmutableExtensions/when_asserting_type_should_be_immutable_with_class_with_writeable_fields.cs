using System;
using Machine.Specifications;

namespace Dolittle.Immutability.for_ImmutableExtensions
{
    public class when_asserting_type_should_be_immutable_with_class_with_writeable_fields
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => typeof(class_with_only_writeable_fields).ShouldBeImmutable());

        It should_throw_writeable_immutable_properties_found = () => exception.ShouldBeOfExactType<WriteableImmutableFieldsFound>();
    }
}