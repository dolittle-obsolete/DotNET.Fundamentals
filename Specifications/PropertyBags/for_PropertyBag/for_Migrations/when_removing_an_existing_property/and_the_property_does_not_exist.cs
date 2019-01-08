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

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_removing_an_existing_property
{

    [Subject(typeof(RemoveProperty),"Perform")]   
    public class and_the_property_does_not_exist
    {
        static RemoveProperty remove_property;
        static NullFreeDictionary<string,object> target;

        Establish context = () => 
        {
            remove_property = new RemoveProperty("MissingProperty");
            target = new NullFreeDictionary<string, object>();
        };

        Because of = () => remove_property.Perform(target);

        It should_do_nothing = () => target.ContainsKey("MissingProperty").ShouldBeFalse();
    }    
}