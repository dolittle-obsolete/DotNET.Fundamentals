using Machine.Specifications;

namespace Dolittle.Serialization.Protobuf.for_MessageDescription
{
    public class when_building_for_type
    {
        static IMessageDescriptionBuilderFor<class_with_properties>  builder;

        Because of = () => MessageDescription.For<class_with_properties>(b => builder = b);

        It should_pass_a_builder_to_callback = () => builder.ShouldNotBeNull();
    }
}