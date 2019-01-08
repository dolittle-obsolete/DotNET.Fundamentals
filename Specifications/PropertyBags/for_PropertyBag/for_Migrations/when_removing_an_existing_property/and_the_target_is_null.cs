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
    public class and_the_target_is_null
    {
        static RemoveProperty remove_property;
        static Exception exception;

        Establish context = () => remove_property = new RemoveProperty("Spamalot!");

        Because of = () => exception = Catch.Exception(() => remove_property.Perform(null));

        It should_fail_with_an_invalid_migration_source_exception = () => exception.ShouldBeOfExactType<InvalidMigrationSource>();
    }
}