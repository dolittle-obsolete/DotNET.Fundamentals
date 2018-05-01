using Machine.Specifications;
using Moq;

namespace Dolittle.Serialization.Protobuf.for_Serializer.given
{
    public class all_dependencies
    {
        protected static Mock<IMessageDescriptions> message_descriptions;
        protected static Mock<IValueConverters> value_converters;

        Establish context = () =>
        {
            message_descriptions = new Mock<IMessageDescriptions>();
            value_converters = new Mock<IValueConverters>();
        };
    }
}