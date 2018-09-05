using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable
{
    public class with_an_enumerable_with_complex_elements : given.an_object_factory
    {
        static IObjectFactory factory;
        static EnumerableWithComplexElements enumerable_type;
        static PropertyBag source;
        static EnumerableWithComplexElements result;
        Establish context = () => 
        {
            factory = instance;
            enumerable_type = new EnumerableWithComplexElements();
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<EnumerableWithComplexElements>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<EnumerableWithComplexElements>();
        It should_have_the_same_properties_as_the_source = () => result.Enumerable.ShouldContainOnly(enumerable_type.Enumerable);
    }
}