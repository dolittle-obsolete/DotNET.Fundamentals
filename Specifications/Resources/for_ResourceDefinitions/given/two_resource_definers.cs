using Machine.Specifications;
using Moq;

namespace doLittle.Resources.for_ResourceDefinitions.given
{
    public class two_resource_definers : all_dependencies
    {
        protected static Mock<ICanDefineResource>   first_resource_definer;
        protected static Mock<ICanDefineResource>   second_resource_definer;

        Establish context = () =>
        { 
            first_resource_definer = new Mock<ICanDefineResource>();
            second_resource_definer = new Mock<ICanDefineResource>();
            resource_definers.Add(first_resource_definer.Object);
            resource_definers.Add(second_resource_definer.Object);
        };
    }
}