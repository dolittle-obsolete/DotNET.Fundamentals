namespace Dolittle.PropertyBags.Specs.for_ImmutableTypeConstructorBasedFactory.when_asking_can_build
{
    using Dolittle.PropertyBags.Specs;
    using Machine.Specifications;

    [Subject(typeof(ImmutableTypeConstructorBasedFactory),"can_build")]
    public class for_an_immutable_type_with_multiple_parameter_and_property_bag_constructors
    {
        static ITypeFactory type_factory;
        static bool can_build;
        static bool can_build_generic;
        Establish context = () => 
        {
            type_factory = new ImmutableTypeConstructorBasedFactory();
        };

        Because of = () => 
        {
            can_build = type_factory.CanBuild(typeof(ImmutableWithMultipleParameterAndPropertyBagConstructors));
            can_build_generic = type_factory.CanBuild<ImmutableWithMultipleParameterAndPropertyBagConstructors>();
        };

        It should_indicate_it_can_build_from_the_type = () => can_build.ShouldBeTrue();
        It should_indicate_it_can_build_from_the_generic = () => can_build.ShouldBeTrue();
    }
}