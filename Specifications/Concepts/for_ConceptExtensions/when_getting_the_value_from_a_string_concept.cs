﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;
using Dolittle.Specs.Concepts.given;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptExtensions
{
    [Subject(typeof(ConceptExtensions))]
    public class when_getting_the_value_from_a_string_concept : concepts
    {
        static StringConcept value;
        static string primitive_value = "ten";
        static object returned_value;

        Establish context = () => value = new StringConcept { Value = primitive_value };

        Because of = () => returned_value = value.GetConceptValue();

        It should_get_the_value_of_the_primitive = () => returned_value.ShouldEqual(primitive_value);
    }
}