// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_primitive_elements
{
    public class when_building_mutable_with_enumerable_of_enumerable : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableOfEnumerableOfPrimitive enumerable_type;
        static PropertyBag source;
        static MutableWithEnumerableOfEnumerableOfPrimitive result;

        Establish context = () =>
        {
            factory = instance;
            enumerable_type = new MutableWithEnumerableOfEnumerableOfPrimitive
            {
                Enumerable = new IEnumerable<string>[]
                {
                    new string[]
                    {
                        "new MutableTypeWithDefaultConstructor()"
                    }
                }
            };
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableOfEnumerableOfPrimitive>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableOfEnumerableOfPrimitive>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();
    }
}