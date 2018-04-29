using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_MessageDescription
{
    public class when_creating_with_custom_name
    {
        const string custom_name = "CustomName";
        static MessageDescription result;

        Because of = () => result = new MessageDescription(typeof(class_with_properties), new PropertyDescription[0], custom_name);

        It should_set_custom_name = () => result.Name.ShouldEqual(custom_name);
    }
}