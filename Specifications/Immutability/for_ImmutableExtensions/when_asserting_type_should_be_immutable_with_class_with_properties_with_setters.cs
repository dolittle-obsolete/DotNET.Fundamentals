using System;
using Machine.Specifications;

namespace Dolittle.Immutability.for_ImmutableExtensions
{
    public class when_asserting_type_should_be_immutable_with_class_with_properties_with_setters
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => typeof(class_with_properties_with_setters).ShouldBeImmutable());

        It should_throw_writeable_immutable_properties_found = () => exception.ShouldBeOfExactType<WriteableImmutablePropertiesFound>();
    }

}