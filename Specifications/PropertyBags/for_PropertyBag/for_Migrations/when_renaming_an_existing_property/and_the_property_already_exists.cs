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
    public class and_the_property_already_exists
    {
        static RenameProperty rename;
        static NullFreeDictionary<string,object> target;

        const string original_name = "ExistingProperty";
        const string new_name = "NewName";
        const string property_value = "The Holy Handgrenade of Atioch";

        Establish context = () => 
        {
            rename = new RenameProperty(original_name, new_name);
            target = new NullFreeDictionary<string, object>();
            target.Add(original_name,property_value);
        };

        Because of = () => rename.Perform(target);

        It should_not_have_the_original_property = () => target.ContainsKey(original_name).ShouldBeFalse();
        It should_have_the_new_property = () => target.ContainsKey(new_name).ShouldBeTrue();
        It should_have_the_same_value = () => target[new_name].ShouldEqual(property_value);
    }   
}