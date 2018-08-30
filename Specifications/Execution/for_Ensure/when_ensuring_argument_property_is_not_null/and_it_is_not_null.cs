namespace Execution.for_Ensure.when_ensuring_argument_property_is_not_null
{
    using System;
    using Dolittle.Execution;
    using Machine.Specifications;

    [Subject(typeof(Ensure))]
    public class and_it_is_not_null
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => Ensure.ArgumentPropertyIsNotNull("test","foo", "myValue"));

        It should_not_throw_an_exception = () => exception.ShouldBeNull();
    }
}