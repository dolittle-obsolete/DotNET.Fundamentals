using System;
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
        It should_have_the_null_values_for_the_concepts = () =>
        {           
            (result as PropertyBag)["StringConcept"].ShouldBeNull();
            (result as PropertyBag)["LongConcept"].ShouldBeNull();
        };
        It should_have_keys_for_the_concepts = () =>
        {           
            (result as PropertyBag).ContainsKey("StringConcept").ShouldBeTrue();
            (result as PropertyBag).ContainsKey("LongConcept").ShouldBeTrue();
        };
    }    
}