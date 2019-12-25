// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_PropertyMapExtensions
{
    public class when_specifying_to_property
    {
        static PropertyMap<object, Target> map;

        Establish context = () => map = new PropertyMap<object, Target>(null);

        Because of = () => map.To(t => t.SomeProperty);

        It should_have_a_specified_property_strategy = () => map.Strategy.ShouldBeOfExactType<TargetPropertyMappingStrategy>();
        It should_hold_the_reference_to_the_target_property_in_the_strategy = () => ((TargetPropertyMappingStrategy)map.Strategy).PropertyInfo.ShouldEqual(typeof(Target).GetTypeInfo().GetProperty("SomeProperty"));
    }
}
