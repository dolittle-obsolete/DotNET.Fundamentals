using System;
using Machine.Specifications;

namespace Dolittle.Booting.for_BootStageBuilder
{
    public class when_getting_container_and_it_is_not_set : given.an_empty_boot_stage_builder
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => builder.Container);

        It should_throw_container_not_set_yet = () => result.ShouldBeOfExactType<ContainerNotSetYet>();
    }
}