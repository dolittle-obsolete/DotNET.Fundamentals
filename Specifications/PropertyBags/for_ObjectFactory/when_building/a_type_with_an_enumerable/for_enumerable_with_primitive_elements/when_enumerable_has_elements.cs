// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_primitive_elements
{
    public class when_enumerable_has_elements : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableWithPrimitiveElements enumerable_type;
        static PropertyBag source;
        static MutableWithEnumerableWithPrimitiveElements result;

        Establish context = () =>
        {
            factory = instance;
            enumerable_type = new MutableWithEnumerableWithPrimitiveElements
            {
                Enumerable = new string[]
                {
                    "s1",
                    null,
                    "s2"
                }
            };
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableWithPrimitiveElements>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableWithPrimitiveElements>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();
    }
}