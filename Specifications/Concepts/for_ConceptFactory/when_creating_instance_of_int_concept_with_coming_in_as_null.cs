﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_int_concept_with_coming_in_as_null
    {
        static IntConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(IntConcept), null) as IntConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(0);
    }
}
