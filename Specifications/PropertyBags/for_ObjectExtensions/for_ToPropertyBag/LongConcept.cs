// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    public class LongConcept : ConceptAs<long>
    {
        public LongConcept(long value) => Value = value;

        public static implicit operator LongConcept(long value) => new LongConcept(value);
    }
}