// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class when_map_has_one_property_mapped_and_one_property_unmapped
    {
        static MapWithOneOfTwoPropertiesMapped map;

        Because of = () => map = new MapWithOneOfTwoPropertiesMapped();

        It should_map_with_the_source_property_strategy = () => map.Properties.ToArray()[1].Strategy.ShouldBeOfExactType<SourcePropertyMappingStrategy>();
    }
}
