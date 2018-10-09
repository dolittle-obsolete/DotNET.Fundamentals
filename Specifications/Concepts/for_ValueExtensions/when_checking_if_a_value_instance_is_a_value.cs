namespace Dolittle.Specs.Concepts.for_ValueExtensions
{
    using System;
    using Dolittle.Concepts;
    using Machine.Specifications;

    [Subject(typeof(ValueExtensions),nameof(ValueExtensions.IsValue))]
    public class when_checking_if_is_a_value
    {
        static TestValue test_value;
        static object obj;

        static bool test_value_is_a_value;
        static bool test_value_type_is_a_value;
        static bool obj_is_a_value;
        static bool object_type_is_a_value;
        Establish context = () => 
        {
            test_value = new TestValue();
            obj = new object();
        };

        Because of = () => 
        {
            test_value_is_a_value = test_value.IsValue();
            test_value_type_is_a_value = test_value.GetType().IsValue();
            obj_is_a_value = obj.IsValue();
            object_type_is_a_value = obj.GetType().IsValue();
        };

        It should_be_true_for_the_value_instance = () => test_value_is_a_value.ShouldBeTrue();
        It should_be_true_for_the_value_type = () => test_value_type_is_a_value.ShouldBeTrue();
        It should_be_false_for_a_non_value_instance = () => obj_is_a_value.ShouldBeFalse();
        It should_be_false_for_a_non_value_type = () => object_type_is_a_value.ShouldBeFalse();
    }
}