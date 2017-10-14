using System.Collections.Generic;
using doLittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{

    public class when_initializing_with_source_and_targets
    {
        static IResourceDefinition source;
        static IEnumerable<ResourceServiceTarget> targets;
        static ResourceTargetDefinition resource_target_definition;

        Establish context = () => 
        {
            source = Mock.Of<IResourceDefinition>();
            targets = new [] {
                new ResourceServiceTarget(new ResourceService(typeof(IService)), "something", typeof(ServiceTarget))
            };
        };

        Because of = () => resource_target_definition = new ResourceTargetDefinition(source, targets);

        It should_set_source = () => resource_target_definition.Source.ShouldEqual(source);
        It should_set_targets = () => resource_target_definition.Targets.ShouldContainOnly(targets);
    }
}