using System;
using Dolittle.Concepts;
using Dolittle.Dynamic;
using Machine.Specifications;

namespace Dolittle.Dynamic.for_ObjectExtensions.for_ToPropertyBag
{

    public class LongConcept : ConceptAs<long>
    {
        public LongConcept(long value) => Value = value;
    
        public static implicit operator LongConcept(long value) => new LongConcept(value);
    }
}