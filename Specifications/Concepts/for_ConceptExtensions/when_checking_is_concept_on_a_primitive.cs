using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptExtensions
{
    [Subject(typeof(ConceptExtensions))]
    public class when_checking_is_concept_on_a_primitive : Dolittle.Specs.Concepts.given.concepts
    {
        static bool is_a_concept;

        Because of = () => is_a_concept = 1.GetType().IsConcept();

        It should_not_be_a_concept = () => is_a_concept.ShouldBeFalse();
    }
}