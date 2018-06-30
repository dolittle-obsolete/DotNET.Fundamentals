using Dolittle.Specs.Concepts.given;

namespace Dolittle.Specs.Concepts.for_ConceptAs.given
{
    public class test_concepts
    {
        protected static IntConcept least = 0;
        protected static IntConcept most = 10;
        protected static IntConcept another_instance_of_most = most.Value;
        protected static IntConcept middle = 5;
    }
}