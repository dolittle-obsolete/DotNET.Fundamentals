// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Dolittle.Time;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_complex_elements
{
    public class when_building_immutable_with_enumerable : given.an_object_factory
    {
        static IObjectFactory factory;
        static ImmutableWithEnumerableWithComplexType enumerable_type;
        static PropertyBag source;
        static ImmutableWithEnumerableWithComplexType result;

        Establish context = () =>
        {
            factory = instance;
            enumerable_type = new ImmutableWithEnumerableWithComplexType(new ComplexImmutableWithMultipleParameterConstructor[]
            {
                new ComplexImmutableWithMultipleParameterConstructor(2, "Two", DateTime.Now, new ImmutableWithMultipleParameterConstructor(3, "three", DateTime.Now, null))
            });
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<ImmutableWithEnumerableWithComplexType>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ImmutableWithEnumerableWithComplexType>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();

        It should_have_the_correct_values_in_the_enumerable = () =>
        {
            var complex = result.Enumerable.First();
            var orig = enumerable_type.Enumerable.First();

            complex.DateTimeProperty.LossyEquals(orig.DateTimeProperty).ShouldBeTrue();
            complex.Nested.DateTimeProperty.LossyEquals(orig.Nested.DateTimeProperty).ShouldBeTrue();
            complex.Nested.NullableDateTime.ShouldBeNull();
            orig.Nested.NullableDateTime.ShouldBeNull();
            complex.Nested.IntProperty.ShouldEqual(orig.Nested.IntProperty);
            complex.Nested.StringProperty.ShouldEqual(orig.Nested.StringProperty);
        };
    }
}