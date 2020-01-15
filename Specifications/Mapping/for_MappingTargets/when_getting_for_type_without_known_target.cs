﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_MappingTargets
{
    public class when_getting_for_type_without_known_target : given.no_mapping_targets
    {
        static IMappingTarget result;

        Because of = () => result = targets.GetFor(typeof(string));

        It should_return_the_default_mapping_target = () => result.ShouldBeOfExactType<DefaultMappingTarget>();
    }
}
