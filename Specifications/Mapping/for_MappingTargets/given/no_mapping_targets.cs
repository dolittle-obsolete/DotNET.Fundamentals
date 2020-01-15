// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_MappingTargets.given
{
    public class no_mapping_targets : all_dependencies
    {
        protected static MappingTargets targets;

        Establish context = () =>
        {
            mapping_targets_mock.Setup(m => m.GetEnumerator()).Returns(Array.Empty<IMappingTarget>().AsEnumerable().GetEnumerator());
            targets = new MappingTargets(mapping_targets_mock.Object);
        };
    }
}
