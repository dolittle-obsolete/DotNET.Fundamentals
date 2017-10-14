using doLittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{

    public class when_checking_if_has_service_target_and_it_does_not
    {
        static ResourceTargetDefinition resource_target_definition;
        static bool result;
        Establish context = () => resource_target_definition = new ResourceTargetDefinition(Mock.Of<IResourceDefinition>(), new ResourceServiceTarget[0]);
        Because of = () => result = resource_target_definition.HasServiceTarget("Something");
        It should_not_have_it = () => result.ShouldBeFalse();
    }
}