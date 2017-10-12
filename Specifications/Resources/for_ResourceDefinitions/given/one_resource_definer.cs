using Machine.Specifications;
using Moq;

namespace doLittle.Resources.for_ResourceDefinitions.given
{
    public class one_resource_definer : all_dependencies
    {
        protected static Mock<ICanDefineResource>   resource_definer;

        Establish context = () =>
        { 
            resource_definer = new Mock<ICanDefineResource>();
            resource_definers.Add(resource_definer.Object);
        };
    }
}