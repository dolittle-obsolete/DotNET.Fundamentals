// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Collections;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Migrations.for_PropertyBag.for_Migrations.when_removing_an_existing_property
{
    [Subject(typeof(RemoveProperty), "Perform")]
    public class and_the_property_already_exists
    {
        static RemoveProperty remove_property;
        static NullFreeDictionary<string, object> target;

        Establish context = () =>
        {
            remove_property = new RemoveProperty("ExistingProperty");
            target = new NullFreeDictionary<string, object>
            {
                { "ExistingProperty", "The Holy Handgrenade of Atioch" }
            };
        };

        Because of = () => remove_property.Perform(target);

        It should_remove_the_property = () => target.ContainsKey("ExistingProperty").ShouldBeFalse();
    }
}