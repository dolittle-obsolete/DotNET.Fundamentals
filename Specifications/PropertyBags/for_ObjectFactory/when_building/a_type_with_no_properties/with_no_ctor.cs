// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.an_immutable_with_concepts
{
    [Subject(typeof(ObjectFactory), "Build")]
    public class with_no_ctor : given.an_object_factory
    {
        static IObjectFactory factory;
        static EmptyClass empty;
        static PropertyBag source;
        static object result;

        Establish context = () =>
        {
            factory = instance;
            empty = new EmptyClass();
            source = empty.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(EmptyClass), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<EmptyClass>();
    }
}