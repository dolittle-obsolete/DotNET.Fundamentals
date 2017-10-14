using System;
using System.Collections.Generic;
using doLittle.Resources;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace doLittle.Resources.for_ResourceServiceTarget
{
    public class when_creating_with_target_service_not_assignable_to_source
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => new ResourceServiceTarget(new ResourceService(typeof(IService)), "something", typeof(string)));

        It should_throw_target_service_is_unassignable_to_source_service = () => result.ShouldBeOfExactType<TargetServiceIsUnassignableToSourceService>();
    }
}