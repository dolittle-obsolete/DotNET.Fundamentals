using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_adding_a_new_property
{

    [Subject(typeof(AddNewProperty<>),"Perform")]   
    public class and_the_property_already_exists
    {
        static AddNewProperty<int> add_new_int_property;
        static NullFreeDictionary<string,object> target;

        static Exception exception;

        Establish context = () => 
        {
            add_new_int_property = new AddNewProperty<int>("ExistingProperty",100);
            target = new NullFreeDictionary<string, object>();
            target.Add("ExistingProperty","The Holy Handgrenade of Atioch");
        };

        Because of = () => exception = Catch.Exception(() => add_new_int_property.Perform(target));

        It should_fail_with_a_duplicate_property_exception = () => exception.ShouldBeOfExactType<DuplicateProperty>();
    }   
}