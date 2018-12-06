using Machine.Specifications;

namespace Dolittle.Booting.for_BootStageBuilder
{
    public class when_getting_association_that_overrides_an_initial_association : given.initial_associations
    {
        static object override_association = "The Override";
        static object result;

        Establish context = () => builder.Associate(second_key, override_association);

        Because of = () => result = builder.GetAssociation(second_key);

        It should_get_the_association_that_was_initially_set = () => result.ShouldEqual(override_association);
    }    
}