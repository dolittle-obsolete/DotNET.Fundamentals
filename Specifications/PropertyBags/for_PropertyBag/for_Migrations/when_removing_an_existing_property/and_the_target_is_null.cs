// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_removing_an_existing_property
{
    [Subject(typeof(RemoveProperty), "Perform")]
    public class and_the_target_is_null
    {
        static RemoveProperty remove_property;
        static Exception exception;

        Establish context = () => remove_property = new RemoveProperty("Spamalot!");

        Because of = () => exception = Catch.Exception(() => remove_property.Perform(null));

        It should_fail_with_migration_source_cannot_be_null = () => exception.ShouldBeOfExactType<MigrationSourceCannotBeNull>();
    }
}