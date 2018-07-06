using System;
using Dolittle.Concepts;
using Dolittle.Dynamic;
using Machine.Specifications;

namespace Dolittle.Dynamic.for_ObjectExtensions.for_ToPropertyBag
{

    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;
    
        public static implicit operator StringConcept(string value) => new StringConcept(value);
    }
}