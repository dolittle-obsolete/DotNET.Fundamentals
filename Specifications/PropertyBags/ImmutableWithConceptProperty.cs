// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithConceptProperty : Value<ImmutableWithConceptProperty>
    {
        public ImmutableWithConceptProperty(IntConcept intProperty, StringConcept stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }

        public int IntProperty { get; }

        public string StringProperty { get; }
    }
}