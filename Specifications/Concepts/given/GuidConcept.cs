using System;
using Dolittle.Concepts;

namespace Dolittle.Specs.Concepts.given
{
    public class GuidConcept : ConceptAs<Guid>
    {
        public static implicit operator GuidConcept(Guid value)
        {
            return new GuidConcept { Value = value };
        }
    }
}