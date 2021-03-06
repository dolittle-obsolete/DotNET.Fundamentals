// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Immutability.for_ImmutableExtensions
{
    public class when_checking_for_immutability_with_class_with_readonly_fields
    {
        static bool result;

        Because of = () => result = typeof(class_with_readonly_fields).IsImmutable();

        It should_be_considered_mutable = () => result.ShouldBeFalse();
    }
}