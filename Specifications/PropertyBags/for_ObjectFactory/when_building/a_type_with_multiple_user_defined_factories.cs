// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class a_type_with_multiple_user_defined_factories : given.an_object_factory
    {
        static IObjectFactory factory;
        static RequiresSpecificConstructionByUser user_defined_type;
        static PropertyBag source;
        static Exception exception;

        Establish context = () =>
        {
            factory = instance_with_two_factories_for_the_same_type;
            user_defined_type = new RequiresSpecificConstructionByUser();
            source = user_defined_type.ToPropertyBag();
        };

        Because of = () => exception = Catch.Exception(() => factory.Build(typeof(RequiresSpecificConstructionByUser), source));

        It should_fail = () => exception.ShouldNotBeNull();
        It should_indicate_that_the_type_has_multiple_factories = () => exception.ShouldBeOfExactType<MultipleFactoriesForType>();
    }
}