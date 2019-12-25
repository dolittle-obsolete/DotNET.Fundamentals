// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_complex_elements
{
    public class when_enumerable_property_is_null : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableWithComplexElements enumerable_type;
        static PropertyBag source;
        static MutableWithEnumerableWithComplexElements result;
        Establish context = () =>
        {
            factory = instance;
            enumerable_type = new MutableWithEnumerableWithComplexElements();
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableWithComplexElements>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableWithComplexElements>();

        It enumerable_should_be_null = () => result.Enumerable.ShouldBeNull();
    }
}