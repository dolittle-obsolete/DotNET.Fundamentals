namespace Dolittle.Execution.for_Ensure.when_ensuring_nullable_property_is_not_null
{
    using System;
    using Machine.Specifications;

    [Subject(typeof(Ensure))]
    public class and_it_is_not_null
    {
        const string property_name = "test";
        static Nullable<int> property;
        static Exception exception;

        Establish context = () => 
        {
            property = 5;
        };
        Because of = () => exception = Catch.Exception(() => Ensure.IsNotNull(property_name,property));

        It should_not_throw_an_exception = () => exception.ShouldBeNull();
    }
}