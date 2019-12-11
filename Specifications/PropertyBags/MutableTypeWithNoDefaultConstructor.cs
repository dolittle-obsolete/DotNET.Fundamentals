// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableTypeWithNoDefaultConstructor : Value<MutableTypeWithNoDefaultConstructor>
    {
        public MutableTypeWithNoDefaultConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
        }

        public int IntProperty { get; set; }

        public string StringProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public string NotSetFromAConstuctor { get; set; }

        public int AReadOnlyProperty => 10;
    }
}