using System;
using doLittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Resources.for_ResourceTargetDefinition
{
    public class when_initializing_with_ambiguous_service_targets 
    {
        const string target_name = "Something";

        static Exception result;

        Because of = () => result = Catch.Exception(() =>
                new ResourceTargetDefinition(
                    Mock.Of<IResourceDefinition>(),
                    new [] {
                        new ResourceServiceTarget(new ResourceService(typeof(IService)),target_name, typeof(ServiceTarget)),
                        new ResourceServiceTarget(new ResourceService(typeof(IOtherService)),target_name, typeof(OtherServiceTarget))
                    }
                ));

        It should_throw_ambiguous_service_targets = () => result.ShouldBeOfExactType<AmbiguousServiceTargets>();
   }
}