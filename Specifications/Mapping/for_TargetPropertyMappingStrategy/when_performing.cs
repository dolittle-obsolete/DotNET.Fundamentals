﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Mapping.Specs.for_TargetPropertyMappingStrategy
{
    public class when_performing
    {
        const string value = "Fourty Two";
        const string property_name = "SomeProperty";
        static TargetPropertyMappingStrategy strategy;
        static Mock<IMappingTarget> mapping_target_mock;
        static string target;
        static PropertyInfo property;

        Establish context = () =>
        {
            property = typeof(Target).GetProperty(property_name);
            strategy = new TargetPropertyMappingStrategy(property);
            mapping_target_mock = new Mock<IMappingTarget>();
            target = "Some string";
        };

        Because of = () => strategy.Perform(mapping_target_mock.Object, target, value);

        It should_set_value_on_mapping_target = () => mapping_target_mock.Verify(t => t.SetValue(target, property, value), Times.Once());
    }
}
