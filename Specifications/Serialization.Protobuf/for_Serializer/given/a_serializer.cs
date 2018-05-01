using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_Serializer.given
{
    public class a_serializer : all_dependencies
    {
        protected static Serializer serializer;

        Establish context = () => serializer = new Serializer(message_descriptions.Object, value_converters.Object);
    }
}