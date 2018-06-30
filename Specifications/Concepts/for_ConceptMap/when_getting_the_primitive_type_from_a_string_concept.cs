using System;
using Dolittle.Concepts;
using Dolittle.Specs.Concepts.given;
using Machine.Specifications;

namespace Dolittle.Specs.Concepts.for_ConceptMap
{
    [Subject(typeof(ConceptMap))]
    public class when_getting_the_primitive_type_from_a_string_concept
    {
        static Type result;

        Because of = () => result = ConceptMap.GetConceptValueType(typeof(StringConcept));

        It should_get_a_string = () => result.ShouldEqual(typeof(string));
    }
}