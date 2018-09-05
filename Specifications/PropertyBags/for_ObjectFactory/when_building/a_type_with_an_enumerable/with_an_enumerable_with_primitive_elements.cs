using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable
{
    public class with_an_enumerable_with_primitive_elements : given.an_object_factory
    {
        static IObjectFactory factory;
        static EnumerableWithPrimitiveElements enumerable_type;
        static PropertyBag source;
        static EnumerableWithPrimitiveElements result;
        Establish context = () => 
        {
            factory = instance;
            enumerable_type = new EnumerableWithPrimitiveElements();
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<EnumerableWithPrimitiveElements>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<EnumerableWithPrimitiveElements>();
        It should_have_the_same_properties_as_the_source = () => result.Enumerable.ShouldContainOnly(enumerable_type.Enumerable);
    }
}