using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_MessageDescriptions
{
    public class when_checking_for_non_existing_message_description_for_type : given.no_message_descriptions
    {
        static bool result;
        Because of = () => result = message_descriptions.HasFor<class_with_properties>();

        It should_not_have_a_description = () => result.ShouldBeFalse();
    }
    
}