namespace Reflection.for_TypeExtensions.for_IsImmutable
{
    using Machine.Specifications;
    using Dolittle.Reflection;

    public class when_asking_is_immutable
    {
        static bool class_with_properties_with_no_setter_is_immutable;
        static bool class_with_properties_with_setters_is_immutable;
        static bool class_with_properties_with_private_setters_is_immutable;
        static bool class_with_mixed_properties_is_immutable;
        Because of = () => 
        {
            class_with_properties_with_no_setter_is_immutable = typeof(ClassWithPropertiesWithNoSetter).IsImmutable();
            class_with_properties_with_setters_is_immutable = typeof(ClassWithPropertiesWithSetters).IsImmutable();
            class_with_properties_with_private_setters_is_immutable = typeof(ClassWithPropertiesWithPrivateSetters).IsImmutable();
            class_with_mixed_properties_is_immutable = typeof(ClassWithMixedProperties).IsImmutable();
        };

        It should_be_true_for_a_class_with_properties_with_no_setter = () => class_with_properties_with_no_setter_is_immutable.ShouldBeTrue();
        It should_be_false_for_a_class_with_properties_with_setters = () => class_with_properties_with_setters_is_immutable.ShouldBeFalse();
        It should_be_false_for_a_class_with_properties_with_private_setters = () => class_with_properties_with_private_setters_is_immutable.ShouldBeFalse();
        It should_be_false_for_a_class_with_mixed_properties = () => class_with_mixed_properties_is_immutable.ShouldBeFalse();
    }
}