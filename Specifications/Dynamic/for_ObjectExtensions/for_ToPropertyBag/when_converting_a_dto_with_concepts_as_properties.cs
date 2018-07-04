using Dolittle.Dynamic;
using Machine.Specifications;

namespace Dolittle.Dynamic.for_ObjectExtensions.for_ToPropertyBag
{
    [Subject("ToPropertyBag")]
    public class when_converting_a_dto_with_concepts_as_properties
    {
        static ConceptDto source;
        static dynamic result;

        Establish context = () => { source = new ConceptDto{ StringConcept = "hello", LongConcept = long.MaxValue }; };

        Because of = () => result = source.ToPropertyBag();

        It should_create_a_property_bag = () => (result as PropertyBag).ShouldNotBeNull();
        It should_have_the_primitive_value_from_the_concepts = () =>
        {
            ShouldExtensionMethods.ShouldEqual(result.StringConcept,source.StringConcept.Value);
            ShouldExtensionMethods.ShouldEqual(result.LongConcept,source.LongConcept.Value);
        };
    }
}