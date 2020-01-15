// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class ComplexMutableTypeWithDefaultConstructor : Value<ComplexMutableTypeWithDefaultConstructor>
    {
        public int IntProperty { get; set; }

        public string StringProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public MutableTypeWithDefaultConstructor Nested { get; set; }
    }
}