// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Maps
{
    public class when_asking_if_has_map_for_unknown_combination : given.no_maps
    {
        static bool result;

        Because of = () => result = maps.HasFor(typeof(Source), typeof(Target));

        It should_not_have_a_map = () => result.ShouldBeFalse();
    }
}
