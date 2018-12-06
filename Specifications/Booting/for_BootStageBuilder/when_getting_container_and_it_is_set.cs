using System;
using Dolittle.DependencyInversion;
using Machine.Specifications;

namespace Dolittle.Booting.for_BootStageBuilder
{
    public class when_getting_container_and_it_is_set : given.an_empty_boot_stage_builder
    {
        static IContainer result;
        static IContainer expected;

        Establish context = () =>
        {
            expected = Moq.Mock.Of<IContainer>();
            builder.UseContainer(expected);
        };

        Because of = () => result = builder.Container;

        It should_return_the_used_container = () => result.ShouldEqual(expected);
    }
}