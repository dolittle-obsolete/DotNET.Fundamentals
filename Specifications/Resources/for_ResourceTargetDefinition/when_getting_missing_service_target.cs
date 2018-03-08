using System;
using Dolittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{
    public class when_getting_missing_service_target
    {
        static ResourceTargetDefinition resource_target_definition;
        static Exception result;
        Establish context = () => resource_target_definition = new ResourceTargetDefinition(Mock.Of<IResourceDefinition>(), new ResourceServiceTarget[0]);
        Because of = () => result = Catch.Exception(() => resource_target_definition.GetServiceTarget("Something"));

        It should_throw_missing_service_target = () => result.ShouldBeOfExactType<MissingServiceTarget>();
    }
}