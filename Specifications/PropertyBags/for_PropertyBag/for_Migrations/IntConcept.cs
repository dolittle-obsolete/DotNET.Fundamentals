using Dolittle.Concepts;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations
{
    public class IntConcept : ConceptAs<int>
    {
        public static implicit operator IntConcept(int value)
        {
            return new IntConcept { Value = value };
        }
    }
}