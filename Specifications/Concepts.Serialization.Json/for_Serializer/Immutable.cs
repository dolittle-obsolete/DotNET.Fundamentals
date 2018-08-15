namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{

    public class Immutable : Value<Immutable>
    {
        public ConceptAsString Label { get; }
        public ConceptAsGuid Guid { get; }
        public ConceptAsLong Number { get; set; }

        public Immutable(ConceptAsGuid guid, ConceptAsString label, ConceptAsLong number)
        {
            Label = label;
            Guid = guid;
            Number = number;
        }
    }
}