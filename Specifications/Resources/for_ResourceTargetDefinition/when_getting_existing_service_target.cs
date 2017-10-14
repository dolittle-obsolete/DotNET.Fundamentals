using doLittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{
    public class when_getting_existing_service_target
    {
        const string target_name = "Something";
        
        static ResourceTargetDefinition resource_target_definition;
        static ResourceServiceTarget result;
        Establish context = () => {
            var resourceDefinition = Mock.Of<IResourceDefinition>();
            resource_target_definition = 
                new ResourceTargetDefinition(
                    resourceDefinition,
                    new [] {
                        new ResourceServiceTarget(new ResourceService(typeof(IService)),target_name, typeof(ServiceTarget))
                    });
        };

        Because of = () => result = resource_target_definition.GetServiceTarget(target_name);

        It should_return_the_target = () => result.Name.ShouldEqual(target_name);
    }
}