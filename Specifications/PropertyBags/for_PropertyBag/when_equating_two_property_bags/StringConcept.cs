using System;
using System.Collections.Generic;
using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_PropertyBag.when_equating_two_property_bags
{

    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;
    
        public static implicit operator StringConcept(string value) => new StringConcept(value);
    }
}