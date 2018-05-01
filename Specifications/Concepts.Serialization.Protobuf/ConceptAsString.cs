using System;

namespace Dolittle.Concepts.Serialization.Protobuf.Specs
{
    public class ConceptAsString : ConceptAs<String>
    {
        public static implicit operator ConceptAsString(string value)
        {
            return new ConceptAsString { Value = value };
        }
    }
}