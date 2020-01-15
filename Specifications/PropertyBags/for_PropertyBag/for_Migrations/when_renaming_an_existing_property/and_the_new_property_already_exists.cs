// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_renaming_an_existing_property
{
    [Subject(typeof(RenameProperty), "Perform")]
    public class and_the_new_property_already_exists
    {
        static RenameProperty rename;
        static NullFreeDictionary<string, object> target;
        static Exception exception;

        Establish context = () =>
        {
            rename = new RenameProperty("ExistingProperty", "NewName");
            target = new NullFreeDictionary<string, object>
            {
                { "ExistingProperty", "It could be carried by an African swallow..." },
                { "NewName", "But then the African swallow's not migratory..." }
            };
        };

        Because of = () => exception = Catch.Exception(() => rename.Perform(target));

        It should_fail = () => exception.ShouldNotBeNull();
        It should_indicate_that_the_property_is_a_duplicate = () => exception.ShouldBeOfExactType<DuplicateProperty>();
    }
}