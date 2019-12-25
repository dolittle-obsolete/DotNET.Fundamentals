// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_complex_mutable_type_with_default_constructor
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class using_default_constuctor_and_property_setters : given.an_object_factory
    {
        static IObjectFactory factory;
        static ComplexMutableTypeWithDefaultConstructor mutable_type;
        static PropertyBag source;
        static object result;

        Establish context = () =>
        {
            factory = instance;
            var nested = new MutableTypeWithDefaultConstructor
            {
                IntProperty = 42,
                StringProperty = "Forty-Two",
                DateTimeProperty = DateTime.UtcNow.AddDays(1)
            };
            mutable_type = new ComplexMutableTypeWithDefaultConstructor
            {
                IntProperty = 43,
                StringProperty = "Forty-Three",
                DateTimeProperty = DateTime.UtcNow.AddDays(2),
                Nested = nested
            };
            source = mutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(ComplexMutableTypeWithDefaultConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ComplexMutableTypeWithDefaultConstructor>();
        It should_have_the_same_properties_as_the_source = () => (result as ComplexMutableTypeWithDefaultConstructor).ShouldBeAnAccurateRepresentationOf(mutable_type);
    }
}