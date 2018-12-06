using System.Reflection;
using Machine.Specifications;

namespace Dolittle.Immutability.for_ImmutableExtensions
{
    public class when_getting_writeable_properties
    {
        static PropertyInfo[] properties;

        Because of = () => properties = typeof(class_with_properties_with_setters).GetWriteableProperties();

        It should_return_the_writeable_property = () => properties[0].Name.ShouldEqual("property_with_setter");
    }
}