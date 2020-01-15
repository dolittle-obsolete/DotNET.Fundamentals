// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_PropertyBag.when_equating_two_property_bags
{
    [Subject(typeof(PropertyBag), "Equals")]
    public class and_they_are_the_same_reference
    {
        static PropertyBag first;

        static bool is_equal_based_on_equals_method;
        static bool is_equal_based_on_operator;
        static bool have_the_same_hashcode;

        Establish context = () =>
        {
            var dictionary = new NullFreeDictionary<string, object>
            {
                { "string", "with a value" },
                { "integer", 42 },
                { "DateTime", DateTime.UtcNow },
                { "Concept", new StringConcept("A Concept") }
            };

            first = new PropertyBag(dictionary);
        };

        Because of = () =>
        {
            is_equal_based_on_equals_method = first.Equals(first);
            is_equal_based_on_operator = first == first;
            have_the_same_hashcode = first.GetHashCode() == first.GetHashCode();
        };

        It should_be_equal_based_on_the_equals_method = () => is_equal_based_on_equals_method.ShouldBeTrue();
        It should_be_equal_based_on_the_operator = () => is_equal_based_on_operator.ShouldBeTrue();
        It should_have_the_same_hashcode = () => have_the_same_hashcode.ShouldBeTrue();
    }
}