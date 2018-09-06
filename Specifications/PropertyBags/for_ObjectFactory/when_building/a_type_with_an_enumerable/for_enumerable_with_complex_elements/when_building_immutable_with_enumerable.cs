using Machine.Specifications;
namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_complex_elements
{
    public class when_building_immutable_with_enumerable : given.an_object_factory
    {
        static IObjectFactory factory;
        static ImmutableWithEnumerableWithComplexType enumerable_type;
        static PropertyBag source;
        static ImmutableWithEnumerableWithComplexType result;
        Establish context = () => 
        {
            factory = instance;
            enumerable_type = new ImmutableWithEnumerableWithComplexType(new MutableTypeWithDefaultConstructor[]
            {
                new MutableTypeWithDefaultConstructor(){IntProperty = 2}
            });
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<ImmutableWithEnumerableWithComplexType>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ImmutableWithEnumerableWithComplexType>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();

        //TODO: CHeck the actual content of the Enumerable
    }
}