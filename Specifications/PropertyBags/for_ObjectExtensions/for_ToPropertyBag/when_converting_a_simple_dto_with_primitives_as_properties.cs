using System;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{

    [Subject("ToPropertyBag")]
    public class when_converting_a_simple_dto_with_primitives_as_properties
    {
        static SimpleDto source;
        static dynamic result;

        Establish context = () => source = new SimpleDto { String = "hello", Int = 23, DateTime = DateTime.Now };

        Because of = () => result = source.ToPropertyBag();

        It should_create_a_property_bag = () => (result as PropertyBag).ShouldNotBeNull();
        It should_have_the_primitive_properties = () =>
        {
            ShouldExtensionMethods.ShouldEqual(result.String,source.String);
            ShouldExtensionMethods.ShouldEqual(result.Int,source.Int);
            ShouldExtensionMethods.ShouldEqual(result.DateTime,source.DateTime);
        };
    }
}