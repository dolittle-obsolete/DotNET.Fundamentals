// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_complex_immutable
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class with_a_ctor_with_a_single_property_bag_parameter : given.an_object_factory
    {
        static IObjectFactory factory;
        static ComplexImmutableWithMultipleParameterConstructor immutable_type;
        static PropertyBag source;
        static dynamic result;

        Establish context = () =>
        {
            factory = instance;
            immutable_type = new ComplexImmutableWithMultipleParameterConstructor(42, "Forty-Two", DateTime.UtcNow, new ImmutableWithMultipleParameterConstructor(43, "Forty-Three", DateTime.UtcNow.AddDays(1), DateTime.Now));
            source = immutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(ComplexImmutableWithMultipleParameterConstructor), source);

        It should_build_an_instance_of_the_type = () => (result as object).ShouldBeOfExactType<ComplexImmutableWithMultipleParameterConstructor>();

        It should_have_the_same_properties_as_the_source = () =>
        {
            immutable_type.IntProperty.ShouldEqual((int)result.IntProperty);
            immutable_type.StringProperty.ShouldEqual((string)result.StringProperty);
            immutable_type.DateTimeProperty.ToUniversalTime().ShouldBeCloseTo((DateTime)result.DateTimeProperty, TimeSpan.FromMilliseconds(1));
            (immutable_type.Nested as object).ShouldBeOfExactType<ImmutableWithMultipleParameterConstructor>();
            immutable_type.Nested.IntProperty.ShouldEqual((int)result.Nested.IntProperty);
            immutable_type.Nested.StringProperty.ShouldEqual((string)result.Nested.StringProperty);
            immutable_type.Nested.DateTimeProperty.ToUniversalTime().ShouldBeCloseTo((DateTime)result.Nested.DateTimeProperty, TimeSpan.FromMilliseconds(1));
        };
    }
}