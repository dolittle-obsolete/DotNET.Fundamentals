// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableAndPrimitiveType
    {
        public IEnumerable<string> Enumerable { get; set; }

        public string String { get; set; }
    }
}