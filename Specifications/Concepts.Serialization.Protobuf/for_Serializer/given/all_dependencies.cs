using Machine.Specifications;
using Moq;
using Dolittle.Serialization.Protobuf;

namespace Dolittle.Concepts.Serialization.Protobuf.for_Serializer.given
{
    public class all_dependencies
    {
        protected static Mock<IMessageDescriptions> message_descriptions;

        Establish context = () =>
        {
            message_descriptions = new Mock<IMessageDescriptions>();
        };
    }
}