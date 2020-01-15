// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_MappingTargets
{
    public class when_getting_for_type_with_known_target : given.mapping_target_for_string
    {
        static IMappingTarget result;

        Because of = () => result = targets.GetFor(typeof(string));

        It should_return_the_expected_mapping_target = () => result.ShouldEqual(mapping_target);
    }
}
