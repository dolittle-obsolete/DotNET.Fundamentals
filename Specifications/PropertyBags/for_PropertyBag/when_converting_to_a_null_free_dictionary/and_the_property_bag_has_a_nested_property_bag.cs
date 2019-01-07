using System;
using Dolittle.Collections;
using Dolittle.PropertyBags.Specs;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.when_converting_to_a_null_free_dictionary
{
    [Subject(typeof(PropertyBag),"ToNullFreeDictionary")]   
    public class and_the_property_bag_has_a_nested_property_bag
    {
        static dynamic property_bag;
        static dynamic nested_property_bag;
        static NullFreeDictionary<string,object> source;
        static NullFreeDictionary<string,object> nested_source;
        static NullFreeDictionary<string,object> from_property_bag;

        Establish context = () => 
        {
            nested_source = new NullFreeDictionary<string,object>
            {
                {"NestedStringValue", "hello"},
                {"NestedIntegerValue", 67 }
            };
            nested_property_bag = new PropertyBag(nested_source);

            source = new NullFreeDictionary<string, object>
            { 
                {"StringValue", "with a value"},
                {"IntegerValue", 42},
                {"DateTimeValue", DateTime.UtcNow},
                {"ConceptValue", new StringConcept("A Concept")},
                {"NestedValue", nested_property_bag}
            };

            property_bag = new PropertyBag(source);
        };

        Because of = () => from_property_bag = property_bag.ToNullFreeDictionary();
        It should_create_a_null_free_dictionary_with_values_from_the_property_bag = () => 
        {
            from_property_bag["StringValue"].ShouldEqual(property_bag.StringValue as string);
            from_property_bag["IntegerValue"].ShouldEqual((int)property_bag.IntegerValue);
            from_property_bag["DateTimeValue"].ShouldEqual((DateTime)property_bag.DateTimeValue);
            from_property_bag["ConceptValue"].ShouldEqual(property_bag.ConceptValue as StringConcept);

            var nested = from_property_bag["NestedValue"] as NullFreeDictionary<string,object>;
            nested["NestedStringValue"].ShouldEqual(property_bag.NestedValue.NestedStringValue as string);
            nested["NestedIntegerValue"].ShouldEqual((int)property_bag.NestedValue.NestedIntegerValue);
        };
    }    
}