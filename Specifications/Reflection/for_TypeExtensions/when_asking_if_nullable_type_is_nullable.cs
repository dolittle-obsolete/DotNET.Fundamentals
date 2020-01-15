﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Reflection.Specs.for_TypeExtensions
{
    public class when_asking_if_nullable_type_is_nullable
    {
        static bool result;

        Because of = () => result = typeof(int?).IsNullable();

        It should_return_true = () => result.ShouldBeTrue();
    }
}
