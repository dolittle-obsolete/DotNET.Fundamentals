// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Reflection;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class when_map_maps_one_property
    {
        static MyMap map;

        Because of = () => map = new MyMap();

        It should_hold_the_mapped_property = () => map.Properties.First().From.ShouldEqual(typeof(Source).GetTypeInfo().GetProperty("SomeProperty"));
    }
}
