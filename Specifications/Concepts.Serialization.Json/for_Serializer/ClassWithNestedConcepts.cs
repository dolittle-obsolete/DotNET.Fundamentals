namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{
    public class ClassWithNestedConcepts : Value<ClassWithNestedConcepts>
    {
        public ConceptAsGuid GuidConcept { get; set; }
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
        public ClassWithConcepts NestedConcepts { get; set; }
    }
}