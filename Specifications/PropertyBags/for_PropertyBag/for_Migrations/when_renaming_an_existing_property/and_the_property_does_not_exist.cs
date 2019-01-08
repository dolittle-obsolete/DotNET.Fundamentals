using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.PropertyBags;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_renaming_an_existing_property
{

    [Subject(typeof(RenameProperty),"Perform")]   
    public class and_the_property_does_not_exist
    {
        static RenameProperty rename;
        static NullFreeDictionary<string,object> target;
        static Exception exception;

        Establish context = () => 
        {
            rename = new RenameProperty("ExistingProperty","NewName");
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => exception = Catch.Exception(() => rename.Perform(target));

        It should_fail = () => exception.ShouldNotBeNull();
        It should_indicate_that_the_property_is_missing = () => exception.ShouldBeOfExactType<MissingProperty>();
    }  
}