using System;
using Dolittle.Concepts;

namespace Dolittle.Concepts.Serialization.Json.Specs
{
    public class ConceptAsGuid : ConceptAs<Guid>
    {
        public static implicit operator ConceptAsGuid(Guid guid)
        {
            return new ConceptAsGuid() { Value = guid };
        }
    }
}