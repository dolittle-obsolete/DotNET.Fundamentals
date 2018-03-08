using Dolittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{
    public class when_checking_if_has_service_target_that_it_has
    {
        const string target_name = "Something";
        
        static ResourceTargetDefinition resource_target_definition;
        static bool result;
        Establish context = () => {
            var resourceDefinition = Mock.Of<IResourceDefinition>();
            resource_target_definition = 
                new ResourceTargetDefinition(
                    resourceDefinition,
                    new [] {
                        new ResourceServiceTarget(new ResourceService(typeof(IService)),target_name, typeof(ServiceTarget))
                    });
        };

        Because of = () => result = resource_target_definition.HasServiceTarget(target_name);
        It should_have_it = () => result.ShouldBeTrue();
    }
}