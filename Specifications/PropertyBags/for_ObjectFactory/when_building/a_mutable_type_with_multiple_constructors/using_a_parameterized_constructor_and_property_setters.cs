namespace Dolittle.PropertyBags.Specs.for_ObjectFactory.when_building.a_mutable_type_with_multiple_constructors
{
    using Machine.Specifications;
    using Dolittle.PropertyBags;
    using Dolittle.PropertyBags.Specs;
    using System;

    [Subject(typeof(ObjectFactory), "Build")]
    public class using_a_parameterized_constructor_and_property_setters : given.an_object_factory
    {
        static IObjectFactory factory;
        static MutableTypeWithMultipleConstructors mutable_type_using_int_ctor;
        static MutableTypeWithMultipleConstructors mutable_type_using_string_ctor;
        static PropertyBag source_for_int_ctor;
        static PropertyBag source_for_string_ctor;
        static object result_from_int;
        static object result_from_string;
        Establish context = () => 
        {
            factory = instance;
            mutable_type_using_int_ctor = new MutableTypeWithMultipleConstructors(42);
            mutable_type_using_int_ctor.NullableLong = 180;
            source_for_int_ctor = mutable_type_using_int_ctor.ToPropertyBag();

            mutable_type_using_string_ctor = new MutableTypeWithMultipleConstructors("I think the phrase rhymes with 'Clucking Bell'");
            source_for_string_ctor = mutable_type_using_string_ctor.ToPropertyBag();
        };

        Because of = () => {
            result_from_int = factory.Build(typeof(MutableTypeWithMultipleConstructors), source_for_int_ctor);
            result_from_string = factory.Build(typeof(MutableTypeWithMultipleConstructors), source_for_string_ctor);
        };

        It should_build_an_instance_of_the_type_from_the_int = () => result_from_int.ShouldBeOfExactType<MutableTypeWithMultipleConstructors>();
        It should_build_an_instance_of_the_type_from_the_string = () => result_from_string.ShouldBeOfExactType<MutableTypeWithMultipleConstructors>();
        It should_have_the_same_properties_as_the_source_from_the_int = () => result_from_int.ShouldEqual(mutable_type_using_int_ctor);
        It should_have_the_same_properties_as_the_source_from_the_string = () => result_from_string.ShouldEqual(mutable_type_using_string_ctor);
    }
}