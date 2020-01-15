﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_int_concept_with_coming_in_as_int
    {
        static IntConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(IntConcept), 5) as IntConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(5);
    }
}