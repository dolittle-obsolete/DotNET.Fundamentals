using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_primitive_elements
{
    public class when_building_mutable_with_enumerable_and_primitive_property : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableWithEnumerableAndPrimitiveType complex_type;
        static PropertyBag source;
        static MutableWithEnumerableAndPrimitiveType result;
        Establish context = () => 
        {
            factory = instance;
            complex_type = new MutableWithEnumerableAndPrimitiveType();
            complex_type.Enumerable = new string[]
            {
                "s1",
                null,
                "s2"
            };
            complex_type.String = "string";

            source = complex_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<MutableWithEnumerableAndPrimitiveType>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableWithEnumerableAndPrimitiveType>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();         
    }
}