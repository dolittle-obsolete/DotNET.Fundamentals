using Dolittle.Concepts;

namespace Dolittle.Specs.Concepts.given
{
    public class IntConcept : ConceptAs<int>
    {
        public IntConcept()
        {

        }

        public IntConcept(int value)
        {
            Value = value;
        }

        public static implicit operator IntConcept(int value)
        {
            return new IntConcept { Value = value };
        }
    }
}