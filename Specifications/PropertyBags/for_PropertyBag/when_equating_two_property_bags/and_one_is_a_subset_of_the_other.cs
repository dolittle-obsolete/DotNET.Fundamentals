using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_PropertyBag.when_equating_two_property_bags
{

    [Subject(typeof(PropertyBag),"Equals")]    
    public class and_one_is_a_subset_of_the_other
    {
        static PropertyBag first;
        static PropertyBag second;

        static bool is_equal_based_on_equals_method;
        static bool is_equal_based_on_operator;
        static bool have_the_same_hashcode;

        Establish context = () => 
        {
            var first_dictionary = new NullFreeDictionary<string, object>
            { 
                {"string", "with a value"},
                {"integer", 42},
                {"DateTime", DateTime.UtcNow},
                {"Concept", new StringConcept("A Concept")}
            };

            var second_dictionary = new NullFreeDictionary<string,object>(first_dictionary);
            second_dictionary.Add("AnotherProperty","This is not in the first dictionary");

            first = new PropertyBag(first_dictionary);
            second = new PropertyBag(second_dictionary);
        };

        Because of = () => 
        {
            is_equal_based_on_equals_method = first.Equals(second);
            is_equal_based_on_operator = first == second;
            have_the_same_hashcode = first.GetHashCode() == second.GetHashCode();
        };

        It should_not_be_equal_based_on_the_equals_method = () => is_equal_based_on_equals_method.ShouldBeFalse();
        It should_not_be_equal_based_on_the_operator = () => is_equal_based_on_operator.ShouldBeFalse();
        It should_not_have_the_same_hashcode = () => have_the_same_hashcode.ShouldBeFalse();
    }
}