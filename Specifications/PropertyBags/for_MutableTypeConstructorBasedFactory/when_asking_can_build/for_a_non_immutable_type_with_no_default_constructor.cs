namespace Dolittle.PropertyBags.Specs.for_MutableTypeConstructorBasedFactory.when_asking_can_build
{
    using Dolittle.PropertyBags.Specs;
    using Machine.Specifications;
    using Dolittle.PropertyBags;

    [Subject(typeof(MutableTypeConstructorBasedFactory),"can_build")]
    public class for_a_non_immutable_type_with_no_default_constructor
    {
        static ITypeFactory type_factory;
        static bool can_build;
        static bool can_build_generic;
        Establish context = () => 
        {
            type_factory = new MutableTypeConstructorBasedFactory();
        };

        Because of = () => 
        {
            can_build = type_factory.CanBuild(typeof(MutableTypeWithNoDefaultConstructor));
            can_build_generic = type_factory.CanBuild<MutableTypeWithNoDefaultConstructor>();
        };

        It should_indicate_it_can_build_from_the_type = () => can_build.ShouldBeTrue();
        It should_indicate_it_can_build_from_the_generic = () => can_build.ShouldBeTrue();        
    }
}