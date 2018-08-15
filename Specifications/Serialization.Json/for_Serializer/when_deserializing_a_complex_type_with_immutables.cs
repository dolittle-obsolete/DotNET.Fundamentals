namespace Dolittle.Serialization.Json.Specs.for_Serializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Specifications; 
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    using Serialization.Json;

    [Subject(typeof(ISerializer))]
    public class when_deserializing_a_complex_type_with_immutables : given.a_serializer
    {
        static Complex original;
        static string json_representation;
        static Complex result;

        Establish context = () => 
        {
            var content = new Dictionary<string,object> 
            { 
                { "a_string", "test" },
                { "an_int", 10 }
            };
            original = new Complex(Guid.NewGuid(),new Immutable(Guid.NewGuid(),"Test"), 1967, content);
            json_representation = serializer.ToJson(original);
        };

        Because of = () => result = serializer.FromJson<Complex>(json_representation);

        It should_deserialize_the_json_to_the_an_equal_instance = () => result.ShouldEqual(original);
    }
}