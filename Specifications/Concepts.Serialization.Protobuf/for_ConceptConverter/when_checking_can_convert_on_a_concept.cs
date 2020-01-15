﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Concepts.Serialization.Protobuf.Specs.for_ConceptConverter
{
    [Subject(typeof(ConceptConverter))]
    public class when_checking_can_convert_on_a_concept : given.a_concept_converter
    {
        static bool can_convert;

        Because of = () => can_convert = converter.CanConvert(typeof(ConceptAsGuid));

        It should_be_able_to_convert = () => can_convert.ShouldBeTrue();
    }
}