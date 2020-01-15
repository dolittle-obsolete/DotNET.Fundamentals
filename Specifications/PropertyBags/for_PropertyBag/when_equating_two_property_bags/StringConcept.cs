// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs.for_PropertyBag.when_equating_two_property_bags
{
    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;

        public static implicit operator StringConcept(string value) => new StringConcept(value);
    }
}