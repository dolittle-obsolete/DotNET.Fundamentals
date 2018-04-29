using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_PropertyDescription
{
    public class when_creating_with_default_value
    {
        const string default_value = "The default value";
        static PropertyDescription result;

        Because of = () => result = new PropertyDescription(class_with_propery.some_property, defaultValue:default_value);

        It should_set_default_value = () => result.DefaultValue.ShouldEqual(default_value);
    }
}