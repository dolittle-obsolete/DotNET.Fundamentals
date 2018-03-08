using System;
using Dolittle.Concepts.Serialization.Json;
using Machine.Specifications;

namespace Dolittle.Concepts.Serialization.Json.Specs.for_ConceptConverter
{
    [Subject(typeof(ConceptConverter))]
    public class when_checking_can_convert_on_a_non_concept : given.a_concept_converter
    {
        static bool can_convert;

        Because of = () => can_convert = converter.CanConvert(typeof(Guid));

        It should_not_be_able_to_convert = () => can_convert.ShouldBeFalse();
    }
}