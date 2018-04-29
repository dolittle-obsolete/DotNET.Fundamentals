using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_PropertyDescription
{
    public class when_creating_without_specified_number_twice_for_same_property
    {
        static PropertyDescription first;
        static PropertyDescription second;

        Because of = () =>
        {
            first = new PropertyDescription(class_with_propery.some_property);
            second = new PropertyDescription(class_with_propery.some_property);
        };

        It should_not_hold_zero_as_number_for_first = () => first.Number.ShouldNotEqual(0);
        It should_not_hold_zero_as_number_for_second = () => second.Number.ShouldNotEqual(0);
        It should_hold_same_number_for_first_and_Second = () => first.Number.ShouldEqual(second.Number);
    }
}