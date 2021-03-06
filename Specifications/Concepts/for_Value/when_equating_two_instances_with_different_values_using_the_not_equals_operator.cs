﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_Value
{
    [Subject(typeof(Value<>))]
    public class when_equating_two_instances_with_different_values_using_the_not_equals_operator : given.value_objects
    {
        static bool is_not_equal;

        Because of = () => is_not_equal = first_value != different_value;

        It should_not_be_equal = () => is_not_equal.ShouldBeTrue();
    }
}