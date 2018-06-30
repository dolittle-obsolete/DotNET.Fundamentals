using Dolittle.Concepts;

namespace Dolittle.Specs.Concepts.given
{
    public class LongConcept : ConceptAs<long>
    {
        public static implicit operator LongConcept(long value)
        {
            return new LongConcept { Value = value };
        }
    }
}