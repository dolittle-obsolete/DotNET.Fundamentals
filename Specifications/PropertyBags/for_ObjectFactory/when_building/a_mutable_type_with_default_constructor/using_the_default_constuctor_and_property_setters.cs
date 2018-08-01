namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_mutable_type_with_default_constructor
{
    using Machine.Specifications;
    using Dolittle.PropertyBags;
    using Dolittle.PropertyBags.Specs;
    using System;

    [Subject(typeof(ObjectFactory), "Build")]
    public class using_the_default_constuctor_and_property_setters : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableTypeWithDefaultConstructor mutable_type;
        static PropertyBag source;
        static object result;
        Establish context = () => 
        {
            factory = instance;
            mutable_type = new MutableTypeWithDefaultConstructor();
            mutable_type.IntProperty = 42;
            mutable_type.StringProperty = "Forty-Two";
            mutable_type.DateTimeProperty = DateTime.UtcNow.AddDays(1);
            mutable_type.ConceptProperty = "wibble";
            mutable_type.NullableInt = 1888;
            source = mutable_type.ToPropertyBag();
        };

        Because of = () => result = factory.Build(typeof(MutableTypeWithDefaultConstructor), source);

        It should_build_an_instance_of_the_type = () => result.ShouldBeOfExactType<MutableTypeWithDefaultConstructor>();
        It should_have_the_same_properties_as_the_source = () => result.ShouldEqual(mutable_type);
    }
}