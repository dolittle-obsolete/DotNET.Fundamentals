using Machine.Specifications;

namespace Dolittle.Booting.for_BootStageBuilder
{
    public class when_getting_association_coming_from_initial_association : given.initial_associations
    {
        static object result;

        Because of = () => result = builder.GetAssociation(second_key);

        It should_get_the_association_that_was_initially_set = () => result.ShouldEqual(second_value);
    }
}