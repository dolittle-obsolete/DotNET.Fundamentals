using System;
using System.Collections.Generic;
using Dolittle.Serialization.Json;
using Dolittle.Concepts.Serialization.Json.Specs;
using Machine.Specifications;
using System.Linq;

namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{

    [Subject(typeof(ISerializer))]
    public class when_deserializing_a_dictionary_with_concepts_key_and_immutable_as_value : given.a_serializer
    {
        static Dictionary<ConceptAsGuid,Immutable>  original;
        static string json_representation;
        static Dictionary<ConceptAsGuid,Immutable>  result;

        Establish context = () => 
        {
            original = new Dictionary<ConceptAsGuid,Immutable> 
            { 
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new Immutable(Guid.NewGuid(),"first", 100) },
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new Immutable(Guid.NewGuid(),"second", 200) }
            };
            json_representation = serializer.ToJson(original);
        };

        Because of = () => 
        {
            result = serializer.FromJson<Dictionary<ConceptAsGuid,Immutable>>(json_representation);
        };

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