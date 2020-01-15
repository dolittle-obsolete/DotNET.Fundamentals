// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Mapping;
using Machine.Specifications;

namespace Dolittle.Mapping.Specs.for_MappingTargetFor
{
    public class when_asking_for_target_type
    {
        static StringMappingTarget target;

        Establish context = () => target = new StringMappingTarget();

        It should_return_the_type_given_as_generic_parameter = () => target.TargetType.ShouldEqual(typeof(string));
    }
}
