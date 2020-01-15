// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_MutableTypeSetterBasedFactory.when_asking_can_build
{
    [Subject(typeof(MutableTypeSetterBasedFactory), "can_build")]
    public class for_an_immutable_type_with_a_multiple_parameter_constructor
    {
        static ITypeFactory type_factory;
        static bool can_build;
        static bool can_build_generic;
        Establish context = () => type_factory = new MutableTypeSetterBasedFactory();

        Because of = () =>
        {
            can_build = type_factory.CanBuild(typeof(ImmutableWithMultipleParameterConstructor));
            can_build_generic = type_factory.CanBuild<ImmutableWithMultipleParameterConstructor>();
        };

        It should_indicate_it_cannot_build_from_the_type = () => can_build.ShouldBeFalse();
        It should_indicate_it_cannot_build_from_the_generic = () => can_build.ShouldBeFalse();
    }
}