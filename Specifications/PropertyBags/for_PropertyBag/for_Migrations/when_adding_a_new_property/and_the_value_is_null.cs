/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_value_is_null
    {
        static AddNewProperty<string> add_new_property;
        static NullFreeDictionary<string,object> target;

        static Exception exception;

        Establish context = () => 
        {
            target = new NullFreeDictionary<string, object>();
            add_new_property = new AddNewProperty<string>("NewProperty",null);
        };

        Because of = () => exception = Catch.Exception(() => add_new_property.Perform(target));

        It should_not_fail = () => exception.ShouldBeNull();
        It should_not_add_the_property = () => target.ContainsKey("NewProperty").ShouldBeFalse();
    }
}