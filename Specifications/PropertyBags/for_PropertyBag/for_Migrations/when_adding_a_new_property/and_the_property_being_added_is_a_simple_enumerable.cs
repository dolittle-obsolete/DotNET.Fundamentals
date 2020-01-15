// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Collections;
using Dolittle.PropertyBags.Migrations;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations.when_adding_a_new_property
{
    [Subject(typeof(AddNewProperty<>), "Perform")]
    public class and_the_property_being_added_is_a_simple_enumerable
    {
        static AddNewProperty<int[]> add_new_property;
        static NullFreeDictionary<string, object> target;
        static int[] values;

        Establish context = () =>
        {
            values = new[] { 1, 2, 3 };
            add_new_property = new AddNewProperty<int[]>("AddedProperty", values);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedProperty").ShouldBeTrue();

        It should_add_the_correct_value = () =>
        {
            var arr = target["AddedProperty"] as object[];
            arr.Select(i => (int)i).ShouldContainOnly(values);
        };
    }
}