using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{

    public class when_creating_instance_of_concept_with_no_default_constructor
    {
        const long long_value = 42;

        static ConceptWithNoDefaultConstructor result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(ConceptWithNoDefaultConstructor), long_value) as ConceptWithNoDefaultConstructor;

        It should_hold_the_correct_long_value = () => result.Value.ShouldEqual(long_value);
    }
}
