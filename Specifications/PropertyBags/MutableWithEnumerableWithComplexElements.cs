// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableWithComplexElements
    {
        public IEnumerable<ComplexImmutableWithMultipleParameterConstructor> Enumerable { get; set; }
    }
}