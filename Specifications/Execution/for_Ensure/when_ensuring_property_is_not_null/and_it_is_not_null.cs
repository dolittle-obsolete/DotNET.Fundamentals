// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Execution.for_Ensure.when_ensuring_property_is_not_null
{
    [Subject(typeof(Ensure))]
    public class and_it_is_not_null
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => Ensure.IsNotNull("test", "foo"));

        It should_not_throw_an_exception = () => exception.ShouldBeNull();
    }
}