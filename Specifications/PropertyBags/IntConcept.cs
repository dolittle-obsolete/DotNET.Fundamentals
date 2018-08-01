namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;

    public class IntConcept : ConceptAs<int>
    {
        public static implicit operator IntConcept(int value)
        {
            return new IntConcept { Value = value };
        }
    }
}