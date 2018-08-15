using System;
using System.Collections.Generic;
using Dolittle.Serialization.Json;
using Dolittle.Concepts.Serialization.Json.Specs;
using Machine.Specifications;
using System.Linq;

namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{
    [Subject(typeof (Serializer))]
    public class when_serialzing_a_type_with_concepts : given.a_serializer
    {
        static ClassWithConcepts to_serialize;
        static string serialized_version;

        Establish context = () =>
                                {
                                    to_serialize = new ClassWithConcepts()
                                                       {
                                                           GuidConcept =  Guid.NewGuid(),
                                                           StringConcept = "BlahBlahBlah",
                                                           LongConcept = long.MaxValue
                                                       };
                                };

        Because of = () => serialized_version = serializer.ToJson(to_serialize);

        It should_contain_the_guid_value = () => serialized_version.IndexOf(to_serialize.GuidConcept.Value.ToString()).ShouldBeGreaterThan(0);
        It should_contain_the_long_value = () => serialized_version.IndexOf(to_serialize.LongConcept.Value.ToString()).ShouldBeGreaterThan(0);
        It should_contain_the_string_value = () => serialized_version.IndexOf(to_serialize.StringConcept.Value).ShouldBeGreaterThan(0);
    }

    [Subject(typeof(ISerializer))]
    public class when_deserializing_a_dictionary_with_concepts_key_and_object_type_as_value : given.a_serializer
    {
        static Dictionary<ConceptAsGuid,object>  original;
        static string json_representation;
        static Dictionary<ConceptAsGuid,object>  result;

        Establish context = () => 
        {
            original = new Dictionary<ConceptAsGuid,object> 
            { 
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new Immutable(Guid.NewGuid(),"first", 100) },
                { new ConceptAsGuid{ Value = Guid.NewGuid() }, new Immutable(Guid.NewGuid(),"second", 200) }
            };
            json_representation = serializer.ToJson(original);
        };

        Because of = () => 
        {
            result = serializer.FromJson<Dictionary<ConceptAsGuid,object>>(json_representation);
        };

        It should_have_the_same_keys_in_the_deserialized_version = () => 
        {
            result.Keys.First().ShouldEqual(original.Keys.First());
            result.Keys.Last().ShouldEqual(original.Keys.Last());
        };

        It should_not_have_the_same_values_in_the_deserialized_version_when_they_are_of_type_object = () => 
        {
            result.Values.First().ShouldNotEqual(original.Values.First());
            result.Values.Last().ShouldNotEqual(original.Values.Last());
        };
    }

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

    [Subject(typeof(ISerializer))]
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

        Because of = () => 
        {
            result = serializer.FromJson<Dictionary<ConceptAsGuid,ClassWithConcepts>>(json_representation);
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