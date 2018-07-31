using Dolittle.Concepts;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{

    public class ConceptWithNoDefaultConstructor : ConceptAs<long>
    {
        public ConceptWithNoDefaultConstructor(long value) => Value = value;
    }
}
