// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_renaming_an_existing_property
{
    [Subject(typeof(RenameProperty), "Perform")]
    public class and_the_target_is_null
    {
        static RenameProperty rename;
        static Exception exception;

        Establish context = () => rename = new RenameProperty("MissingProperty", "NewName");

        Because of = () => exception = Catch.Exception(() => rename.Perform(null));

        It should_fail_with_an_invalid_migration_source_exception = () => exception.ShouldBeOfExactType<InvalidMigrationSource>();
    }
}