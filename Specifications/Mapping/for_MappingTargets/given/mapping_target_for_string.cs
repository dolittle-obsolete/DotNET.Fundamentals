// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_MappingTargets.given
{
    public class mapping_target_for_string : all_dependencies
    {
        protected static StringMappingTarget mapping_target;
        protected static MappingTargets targets;

        Establish context = () =>
        {
            mapping_target = new StringMappingTarget();
            mapping_targets_mock.Setup(m => m.GetEnumerator()).Returns(new[] { mapping_target }.AsEnumerable().GetEnumerator());
            targets = new MappingTargets(mapping_targets_mock.Object);
        };
    }
}
