namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_complex_mutable_type_with_default_constructor
{
    using Machine.Specifications;
    using Dolittle.PropertyBags;
    using Dolittle.PropertyBags.Specs;
    using System;

    [Subject(typeof(ObjectFactory), "Build")]
    public class using_default_constuctor_and_property_setters : given.an_object_factory
    {
        static IObjectFactory factory;
        static ComplexMutableTypeWithDefaultConstructor mutable_type;
        static PropertyBag source;
        static object result;
        Establish context = () => 
        {
            factory = instance;
            var nested = new MutableTypeWithDefaultConstructor();
            nested.IntProperty = 42;
            nested.StringProperty = "Forty-Two";
            nested.DateTimeProperty = DateTime.UtcNow.AddDays(1);
            mutable_type = new ComplexMutableTypeWithDefaultConstructor();
            mutable_type.IntProperty = 43;
            mutable_type.StringProperty = "Forty-Three";
            mutable_type.DateTimeProperty = DateTime.UtcNow.AddDays(2);
            mutable_type.Nested = nested;
            source = mutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(ComplexMutableTypeWithDefaultConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<ComplexMutableTypeWithDefaultConstructor>();
        //It should_have_the_same_properties_as_the_source = () => (result as ComplexMutableTypeWithDefaultConstructor).ShouldBeAnAccurateRepresentationOf(mutable_type);
    }
}