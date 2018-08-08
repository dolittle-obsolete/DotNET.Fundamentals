namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_complex_immutable
{
    using Machine.Specifications;
    using Dolittle.PropertyBags;
    using Dolittle.PropertyBags.Specs;
    using System;

    [Subject(typeof(ObjectFactory), "Build")]
    public class with_a_ctor_with_a_single_property_bag_parameter : given.an_object_factory
    {
        static IObjectFactory factory;
        static ComplexImmutableWithMultipleParameterConstructor immutable_type;
        static PropertyBag source;
        static object result;
        Establish context = () => 
        {
            factory = instance;
            immutable_type = new ComplexImmutableWithMultipleParameterConstructor(42,"Forty-Two",DateTime.UtcNow, new ImmutableWithMultipleParameterConstructor(43,"Forty-Three",DateTime.UtcNow.AddDays(1), DateTime.Now));
            source = immutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(ComplexImmutableWithMultipleParameterConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ComplexImmutableWithMultipleParameterConstructor>();
        It should_have_the_same_properties_as_the_source = () => result.ShouldEqual(immutable_type);
    }
}