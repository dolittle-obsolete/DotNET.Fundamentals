using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_removing_an_existing_property
{

    [Subject(typeof(RemoveProperty),"Perform")]   
    public class and_the_property_already_exists
    {
        static RemoveProperty remove_property;
        static NullFreeDictionary<string,object> target;

        Establish context = () => 
        {
            remove_property = new RemoveProperty("ExistingProperty");
            target = new NullFreeDictionary<string, object>();
            target.Add("ExistingProperty","The Holy Handgrenade of Atioch");
        };

        Because of = () => remove_property.Perform(target);

        It should_remove_the_property = () => target.ContainsKey("ExistingProperty").ShouldBeFalse();
    }   
}