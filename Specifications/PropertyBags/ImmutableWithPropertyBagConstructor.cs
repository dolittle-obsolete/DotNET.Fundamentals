// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;
using Dolittle.Time;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithPropertyBagConstructor : Value<ImmutableWithPropertyBagConstructor>
    {
        public ImmutableWithPropertyBagConstructor(PropertyBag propertyBag)
        {
            IntProperty = (int)propertyBag[nameof(IntProperty)];
            StringProperty = (string)propertyBag[nameof(StringProperty)];
            DateTimeProperty = ((long)propertyBag[nameof(DateTimeProperty)]).ToDateTime();
        }

        public int IntProperty { get; }

        public string StringProperty { get; }

        public DateTime DateTimeProperty { get; }
    }
}