namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{
    public class ClassWithConcepts : Value<ClassWithConcepts>
    {
        public ConceptAsGuid GuidConcept { get; set; }
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
    }
}