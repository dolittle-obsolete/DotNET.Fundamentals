// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Dolittle.PropertyBags.Specs;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.when_converting_to_a_null_free_dictionary
{
    [Subject(typeof(PropertyBag), "ToNullFreeDictionary")]
    public class and_the_property_bag_has_simple_values
    {
        static PropertyBag property_bag;
        static NullFreeDictionary<string, object> source;
        static NullFreeDictionary<string, object> from_property_bag;

        Establish context = () =>
        {
            source = new NullFreeDictionary<string, object>
            {
                { "string", "with a value" },
                { "integer", 42 },
                { "DateTime", DateTime.UtcNow },
                { "Concept", new StringConcept("A Concept") }
            };

            property_bag = new PropertyBag(source);
        };

        Because of = () => from_property_bag = property_bag.ToNullFreeDictionary();
        It should_create_a_null_free_dictionary_with_values_from_the_property_bag = () => from_property_bag.ShouldEqual(source);
    }
}