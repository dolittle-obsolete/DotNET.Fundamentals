/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Dolittle.PropertyBags.Migrations;
using Dolittle.Time;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_property_being_added_is_a_simple_enumerable
    {
        static AddNewProperty<int[]> add_new_property;
        static NullFreeDictionary<string,object> target;
        static int[] values;

        Establish context = () => 
        {
            values = new [] { 1, 2 ,3 };
            add_new_property = new AddNewProperty<int[]>("AddedProperty",values);
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

    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_property_being_added_is_a_complex_enumerable
    {
        static AddNewProperty<ComplexType[]> add_new_property;
        static NullFreeDictionary<string,object> target;
        static ComplexType[] values;

        Establish context = () => 
        {
            values = new [] { new ComplexType("What is your name?",1), new ComplexType("What is your Quest?",2), new ComplexType("What is your favourite colour?", 3) };
            add_new_property = new AddNewProperty<ComplexType[]>("AddedProperty",values);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedProperty").ShouldBeTrue();
        It should_add_the_correct_value = () => 
        {
            var arr = target["AddedProperty"] as object[];
            arr.Select(ct => (PropertyBag)ct).ShouldContainOnly(values.Select(v => v.ToPropertyBag()));
        };
    }        
}