﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class when_asking_for_target_type
    {
        static MyMap map;
        static Type target_type;

        Establish context = () => map = new MyMap();

        Because of = () => target_type = map.Target;

        It should_be_the_target_type_given_as_generic_parameter = () => target_type.ShouldEqual(typeof(Target));
    }
}
