using System;
using Dolittle.Concepts;

namespace Dolittle.Specs.Concepts.for_StringExtensions
{
    public class ConceptAsGuid : ConceptAs<Guid>
    {
        public static implicit operator ConceptAsGuid(Guid guid)
        {
            return new ConceptAsGuid() { Value = guid };
        }
    }
}