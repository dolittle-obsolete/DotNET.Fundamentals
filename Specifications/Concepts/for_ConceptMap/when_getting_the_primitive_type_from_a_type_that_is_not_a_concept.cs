using System;
using Dolittle.Concepts;
using Machine.Specifications;
using Dolittle.Specs.Concepts.given;

namespace Dolittle.Specs.Concepts.for_ConceptMap
{
    [Subject(typeof(ConceptMap))]
    public class when_getting_the_primitive_type_from_a_type_that_is_not_a_concept
    {
        static Type result;

        Because of = () => result = ConceptMap.GetConceptValueType(typeof(string));

        It should_get_null = () => result.ShouldBeNull();
    }
}