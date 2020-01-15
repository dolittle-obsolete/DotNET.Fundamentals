// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_simple_immutable
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class with_a_ctor_with_multiple_parameters : given.an_object_factory
    {
        static IObjectFactory factory;
        static ImmutableWithMultipleParameterConstructor immutable_type;
        static PropertyBag source;
        static object result;

        Establish context = () =>
        {
            factory = instance;
            immutable_type = new ImmutableWithMultipleParameterConstructor(42, "Forty-Two", DateTime.UtcNow, null);
            source = immutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(ImmutableWithMultipleParameterConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ImmutableWithMultipleParameterConstructor>();
        It should_have_the_same_properties_as_the_source = () => (result as ImmutableWithMultipleParameterConstructor).ShouldBeAnAccurateRepresentationOf(immutable_type);
    }
}