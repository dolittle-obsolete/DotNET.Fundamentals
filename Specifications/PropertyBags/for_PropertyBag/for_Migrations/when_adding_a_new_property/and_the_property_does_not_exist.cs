using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_property_does_not_exist
    {
        static AddNewProperty<int> add_new_int_property;
        static NullFreeDictionary<string,object> target;

        Establish context = () => 
        {
            add_new_int_property = new AddNewProperty<int>("AddedProperty",100);
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => add_new_int_property.Perform(target);

        It should_add_the_property = () => target.ContainsKey("AddedProperty").ShouldBeTrue();
        It should_add_the_correct_value = () => target["AddedProperty"].ShouldEqual(100);
    }    
}