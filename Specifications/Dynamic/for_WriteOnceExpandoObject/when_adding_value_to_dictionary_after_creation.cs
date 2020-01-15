﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Dynamic.Specs.for_WriteOnceExpandoObject
{
    [Subject(typeof(WriteOnceExpandoObject))]
    public class when_adding_value_to_dictionary_after_creation : given.a_write_once_expando_object_without_values
    {
        protected static Exception exception;
        Because of = () => exception = Catch.Exception(() => values["Something"] = 5);

        Behaves_like<a_read_only_container> a_read_only_container;
    }
}
