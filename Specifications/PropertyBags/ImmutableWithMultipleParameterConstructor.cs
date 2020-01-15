// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithMultipleParameterConstructor : Value<ImmutableWithMultipleParameterConstructor>
    {
        public ImmutableWithMultipleParameterConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty, DateTime? nullableDateTime)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
            NullableDateTime = nullableDateTime;
        }

        public int IntProperty { get; }

        public string StringProperty { get; }

        public DateTime DateTimeProperty { get; }

        public DateTime? NullableDateTime { get; }
    }
}