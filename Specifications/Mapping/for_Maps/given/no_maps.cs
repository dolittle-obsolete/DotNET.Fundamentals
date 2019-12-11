// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_Maps.given
{
    public class no_maps : all_dependencies
    {
        protected static Maps maps;

        Establish context = () =>
        {
            map_instances_mock.Setup(m => m.GetEnumerator()).Returns(Array.Empty<IMap>().AsEnumerable().GetEnumerator());
            maps = new Maps(map_instances_mock.Object);
        };
    }
}
