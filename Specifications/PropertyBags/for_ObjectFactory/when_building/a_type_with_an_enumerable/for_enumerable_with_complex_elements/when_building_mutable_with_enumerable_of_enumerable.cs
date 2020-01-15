// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_complex_elements
{
    public class when_building_mutable_with_enumerable_of_enumerable : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableOfEnumerableOfComplex enumerable_type;
        static PropertyBag source;
        static MutableWithEnumerableOfEnumerableOfComplex result;

        Establish context = () =>
        {
            factory = instance;
            enumerable_type = new MutableWithEnumerableOfEnumerableOfComplex
            {
                Enumerable = new IEnumerable<MutableTypeWithDefaultConstructor>[]
                {
                    new MutableTypeWithDefaultConstructor[]
                    {
                        new MutableTypeWithDefaultConstructor()
                            { IntProperty = 2 }
                    }
                }
            };
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableOfEnumerableOfComplex>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableOfEnumerableOfComplex>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();
    }
}