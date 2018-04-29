using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_MessageDescription
{
    public class when_creating_without_custom_name
    {
        static MessageDescription result;

        Because of = () => result = new MessageDescription(typeof(class_with_properties), new PropertyDescription[0]);

        It should_set_name_to_type_name = () => result.Name.ShouldEqual(typeof(class_with_properties).Name);
    }
}