﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_long_concept_with_value_as_string
    {
        const string long_value_as_string = "42";

        static LongConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(LongConcept), long_value_as_string) as LongConcept;

        It should_hold_the_correct_long_value = () => result.Value.ToString().ShouldEqual(long_value_as_string);
    }
}
