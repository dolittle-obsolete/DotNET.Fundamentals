// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithMultipleParameterAndPropertyBagConstructors : Value<ImmutableWithMultipleParameterAndPropertyBagConstructors>
    {
        public ImmutableWithMultipleParameterAndPropertyBagConstructors(int intProperty, string stringProperty, DateTime dateTimeProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
        }

        public ImmutableWithMultipleParameterAndPropertyBagConstructors(PropertyBag propertyBag)
            : this(
            (int)((dynamic)propertyBag).IntProperty,
            (string)((dynamic)propertyBag).StringProperty,
            (DateTime)((dynamic)propertyBag).DateTimeProperty)
        {
        }

        public int IntProperty { get; }

        public string StringProperty { get; }

        public DateTime DateTimeProperty { get; }
    }
}