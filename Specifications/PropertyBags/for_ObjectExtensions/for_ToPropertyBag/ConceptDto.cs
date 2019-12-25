// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    internal class ConceptDto : Value<ConceptDto>
    {
        public StringConcept StringConcept { get; set; }

        public LongConcept LongConcept { get; set; }
    }
}