// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.Specs
{
    public class IntConcept : ConceptAs<int>
    {
        public static implicit operator IntConcept(int value)
        {
            return new IntConcept { Value = value };
        }
    }
}