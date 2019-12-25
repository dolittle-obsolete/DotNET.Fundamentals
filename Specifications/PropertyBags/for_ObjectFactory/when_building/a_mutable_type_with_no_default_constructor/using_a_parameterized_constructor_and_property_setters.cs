// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_mutable_type_with_no_default_constructor
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class using_a_parameterized_constructor_and_property_setters : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableTypeWithNoDefaultConstructor mutable_type;
        static PropertyBag source;
        static object result;
        Establish context = () =>
        {
            factory = instance;
            mutable_type = new MutableTypeWithNoDefaultConstructor(42, "Forty-Two", DateTime.UtcNow)
            {
                NotSetFromAConstuctor = "Wibble"
            };
            source = mutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(MutableTypeWithNoDefaultConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableTypeWithNoDefaultConstructor>();
        It should_have_the_same_properties_as_the_source = () => (result as MutableTypeWithNoDefaultConstructor).ShouldBeAnAccurateRepresentationOf(mutable_type);
    }
}