﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class when_asking_for_source_type
    {
        static MyMap map;
        static Type source_type;

        Establish context = () => map = new MyMap();

        Because of = () => source_type = map.Source;

        It should_be_the_source_type_given_as_generic_parameter = () => source_type.ShouldEqual(typeof(Source));
    }
}
