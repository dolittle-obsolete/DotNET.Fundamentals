using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_primitive_elements
{
    public class when_building_mutable_with_enumerable_and_complex_property : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableAndComplexProperty complex_type;
        static PropertyBag source;
        static MutableWithEnumerableAndComplexProperty result;
        Establish context = () => 
        {
            factory = instance;
            complex_type = new MutableWithEnumerableAndComplexProperty();
            complex_type.Enumerable = new string[]
            {
                "s1",
                null,
                "s2"
            };
            complex_type.Concept = new ImmutableWithConceptProperty(1, "stringConcept");

            source = complex_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableAndComplexProperty>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableAndComplexProperty>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();         
    }
}