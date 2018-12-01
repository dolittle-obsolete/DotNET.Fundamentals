using Machine.Specifications;

namespace Dolittle.Booting.for_BootStageBuilder.given
{
    public class an_empty_boot_stage_builder
    {
        protected static BootStageBuilder builder;

        Establish context = () => builder = new BootStageBuilder();
    }
}