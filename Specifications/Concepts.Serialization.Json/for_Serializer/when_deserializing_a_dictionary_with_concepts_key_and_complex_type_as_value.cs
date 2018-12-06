using System;
using System.Collections.Generic;
using Dolittle.Serialization.Json;
using Dolittle.Concepts.Serialization.Json.Specs;
using Machine.Specifications;
using System.Linq;

namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{

    [Subject(typeof(Serializer))]
    public class when_deserializing_a_dictionary_with_concepts_key_and_complex_type_as_value : given.a_serializer
    {
        static Dictionary<ConceptAsGuid,ClassWithConcepts>  original;
        static string json_representation;
        static Dictionary<ConceptAsGuid,ClassWithConcepts>  result;

        Establish context = () => 
        {
            original = new Dictionary<ConceptAsGuid,ClassWithConcepts> 
            { 
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new ClassWithConcepts { StringConcept = "First", LongConcept = 100, GuidConcept = Guid.NewGuid()} },
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new ClassWithConcepts{ StringConcept = "Second", LongConcept = 100, GuidConcept = Guid.NewGuid()} }
            };
            json_representation = serializer.ToJson(original);
        };

        Because of = () => result = serializer.FromJson<Dictionary<ConceptAsGuid,ClassWithConcepts>>(json_representation);

        It should_have_the_same_keys_in_the_deserialized_version = () => 
        {
            result.Keys.First().ShouldEqual(original.Keys.First());
            result.Keys.Last().ShouldEqual(original.Keys.Last());
        };

        It should_have_the_same_values_in_the_deserialized_version = () => 
        {
            result.Values.First().ShouldEqual(original.Values.First());
            result.Values.Last().ShouldEqual(original.Values.Last());
        };
    }
}