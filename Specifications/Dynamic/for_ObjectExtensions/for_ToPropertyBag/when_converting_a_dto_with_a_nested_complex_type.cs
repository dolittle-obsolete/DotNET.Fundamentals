using System;
using Dolittle.Dynamic;
using Machine.Specifications;

namespace Dolittle.Dynamic.for_ObjectExtensions.for_ToPropertyBag
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
            ShouldExtensionMethods.ShouldEqual(result.String,source.String);
            ShouldExtensionMethods.ShouldEqual(result.Int,source.Int);
            ShouldExtensionMethods.ShouldEqual(result.DateTime,source.DateTime);
        };

        It should_have_the_complex_type_as_a_property_bag = () => (result.Child as PropertyBag).ShouldNotBeNull();

        It should_have_the_correct_properties_on_the_complex_type = () =>
        {
            ShouldExtensionMethods.ShouldEqual(result.Child.String,source.Child.String);
            ShouldExtensionMethods.ShouldEqual(result.Child.Int,source.Child.Int);
            ShouldExtensionMethods.ShouldEqual(result.Child.DateTime,source.Child.DateTime);
        };
    }
}