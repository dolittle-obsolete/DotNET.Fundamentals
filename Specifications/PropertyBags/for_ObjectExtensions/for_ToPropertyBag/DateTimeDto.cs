// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    internal class DateTimeDto : Value<ConceptDto>
    {
        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }
    }
}