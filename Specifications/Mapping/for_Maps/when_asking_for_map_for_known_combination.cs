// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Maps
{
    public class when_asking_for_map_for_known_combination : given.map_for_source_and_target_in_system
    {
        static IMap map;

        Because of = () => map = maps.GetFor(typeof(Source), typeof(Target));

        It should_return_the_expected_map = () => map.ShouldEqual(map_mock.Object);
    }
}
