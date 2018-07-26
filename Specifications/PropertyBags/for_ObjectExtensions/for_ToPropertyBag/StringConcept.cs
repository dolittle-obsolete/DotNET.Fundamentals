using System;
using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{

    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;
    
        public static implicit operator StringConcept(string value) => new StringConcept(value);
    }
}