using System;
using Machine.Specifications;
using Dolittle.PropertyBags;
using Dolittle.Reflection;
using Dolittle.Time;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    [Subject("ToPropertyBag")]
    public class when_converting_a_dto_with_a_nested_complex_type
    {
        static NestedDto source;
        static dynamic result;

        Establish context = () => { source = new NestedDto{ String = "hello", Int = 23, DateTime = DateTime.Now, Child = new SimpleDto { String = "Nested", Int = 46, DateTime = DateTime.Now.AddDays(1)} };};

        Because of = () => result = source.ToPropertyBag();

        It should_create_a_property_bag = () => (result as PropertyBag).ShouldNotBeNull();
        It should_have_the_primitive_properties = () =>
        {
            source.String.ShouldEqual((string)result.String);
            source.Int.ShouldEqual((int)result.Int);
            source.DateTime.ToUnixTimeMilliseconds().ShouldEqual((long)result.DateTime);
        };

        It should_have_the_complex_type_as_a_property_bag = () => (result.Child as PropertyBag).ShouldNotBeNull();

        It should_have_the_correct_properties_on_the_complex_type = () =>
        {
            source.Child.String.ShouldEqual((string)result.Child.String);
            source.Child.Int.ShouldEqual((int)result.Child.Int);
            source.Child.DateTime.ToUnixTimeMilliseconds().ShouldEqual(((long)result.Child.DateTime));
        };
    }
}