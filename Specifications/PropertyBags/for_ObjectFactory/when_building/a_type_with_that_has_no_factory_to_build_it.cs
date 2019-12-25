// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class a_type_with_that_has_no_factory_to_build_it : given.an_object_factory
    {
        static IObjectFactory factory;
        static CannotBeBuiltByAnyNonUserDefinedFactory cannot_be_built;
        static PropertyBag source;
        static Exception exception;
        Establish context = () =>
        {
            factory = instance_with_two_factories_for_the_same_type;
            cannot_be_built = new CannotBeBuiltByAnyNonUserDefinedFactory();
            source = cannot_be_built.ToPropertyBag();
        };

        Because of = () => exception = Catch.Exception(() => factory.Build(typeof(CannotBeBuiltByAnyNonUserDefinedFactory), source));

        It should_fail = () => exception.ShouldNotBeNull();
        It should_indicate_that_the_type_has_no_factories = () => exception.ShouldBeOfExactType<NoFactoriesForType>();
    }
}