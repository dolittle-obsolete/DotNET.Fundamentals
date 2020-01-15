// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_adding_a_new_property
{
    [Subject(typeof(AddNewProperty<>), "Perform")]
    public class and_the_value_is_null
    {
        static AddNewProperty<string> add_new_property;
        static NullFreeDictionary<string, object> target;

        static Exception exception;

        Establish context = () =>
        {
            target = new NullFreeDictionary<string, object>();
            add_new_property = new AddNewProperty<string>("NewProperty", null);
        };

        Because of = () => exception = Catch.Exception(() => add_new_property.Perform(target));

        It should_not_fail = () => exception.ShouldBeNull();
        It should_not_add_the_property = () => target.ContainsKey("NewProperty").ShouldBeFalse();
    }
}