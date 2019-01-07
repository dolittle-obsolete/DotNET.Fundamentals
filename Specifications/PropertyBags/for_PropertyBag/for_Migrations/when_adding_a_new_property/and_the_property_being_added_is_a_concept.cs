using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Dolittle.PropertyBags.Migrations;
using Machine.Specifications;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<IntConcept>),"Perform")]   
    public class and_the_property_being_added_is_a_concept
    {
        static AddNewProperty<IntConcept> add_new_int_property;
        static NullFreeDictionary<string,object> target;

        static Exception exception;

        Establish context = () => 
        {
            add_new_int_property = new AddNewProperty<IntConcept>("AddedProperty",100);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_int_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedProperty").ShouldBeTrue();
        It should_add_the_correct_value = () => target["AddedProperty"].ShouldEqual(100);
    }               
}