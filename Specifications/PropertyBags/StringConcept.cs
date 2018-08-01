namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;

    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;        

        public static implicit operator StringConcept(string value)
        {
            return new StringConcept(value);
        }
    }
}