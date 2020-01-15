﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_a_multi_level_inherited_concept
    {
        const long long_value = 42;

        static InheritedConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(MultiLevelInheritedConcept), long_value) as MultiLevelInheritedConcept;

        It should_hold_the_correct_long_value = () => result.Value.ShouldEqual(long_value);
    }
}