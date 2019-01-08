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

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_renaming_an_existing_property
{

    [Subject(typeof(RenameProperty),"Perform")]   
    public class and_the_new_property_name_contains_invalid_characters
    {
        static RenameProperty rename;
        static NullFreeDictionary<string,object> target;

        static Exception exception;

        Establish context = () => 
        {
            rename = new RenameProperty("Existing","Not @ a ; valid name");
            target = new NullFreeDictionary<string, object>();
            target.Add("Existing","It's not a question of where it grips it...");            
        };

        Because of = () => exception = Catch.Exception(() => rename.Perform(target));

        It should_fail_with_an_invalid_property_name_exception = () => exception.ShouldBeOfExactType<InvalidPropertyName>();
    }            
}