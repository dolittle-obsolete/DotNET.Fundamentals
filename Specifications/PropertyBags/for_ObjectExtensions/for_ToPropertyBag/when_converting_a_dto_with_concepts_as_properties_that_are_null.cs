using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    [Subject("ToPropertyBag")]
    public class when_converting_a_dto_with_concepts_as_properties_that_are_null
    {
        static ConceptDto source;
        static PropertyBag result;

        Establish context = () => source = new ConceptDto();

        Because of = () => result = source.ToPropertyBag();

        It should_create_a_property_bag = () => (result as PropertyBag).ShouldNotBeNull();
        It should_throw_KeyNotFound_exceptions_when_getting_the_concepts = () =>
        {           
            Catch.Exception( () => (result as PropertyBag)["StringConcept"]).ShouldBeOfExactType<KeyNotFoundException>();
            Catch.Exception( () => (result as PropertyBag)["LongConcept"]).ShouldBeOfExactType<KeyNotFoundException>();
        };
        It should_not_have_keys_for_the_concepts = () =>
        {           
            (result as PropertyBag).ContainsKey("StringConcept").ShouldBeFalse();
            (result as PropertyBag).ContainsKey("LongConcept").ShouldBeFalse();
        };
    }    
}