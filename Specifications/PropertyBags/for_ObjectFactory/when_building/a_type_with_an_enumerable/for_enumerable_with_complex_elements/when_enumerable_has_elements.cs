using System;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_type_with_an_enumerable.for_enumerable_with_complex_elements
{
    public class when_enumerable_has_elements : given.an_object_factory
    {
        static IObjectFactory factory;
        static EnumerableWithComplexElements enumerable_type;
        static PropertyBag source;
        static EnumerableWithComplexElements result;
        Establish context = () => 
        {
            factory = instance;
            enumerable_type = new EnumerableWithComplexElements();
            enumerable_type.Enumerable = new ComplexImmutableWithMultipleParameterConstructor[]
            {
                new ComplexImmutableWithMultipleParameterConstructor(1, "34", DateTime.Now, null),

                new ComplexImmutableWithMultipleParameterConstructor(5, "", DateTime.Now, 
                    new ImmutableWithMultipleParameterConstructor(1, "f", DateTime.Now, null))
            };
            source = enumerable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build<EnumerableWithComplexElements>(source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<EnumerableWithComplexElements>();

        It enumerable_should_not_be_null = () => result.Enumerable.ShouldNotBeNull();

        //TODO: CHeck the actual content of the Enumerable

    }
}