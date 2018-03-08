using System;
using Machine.Specifications;

namespace Dolittle.DependencyInversion.Strategies.for_Callback
{
    public class when_asking_for_target_type
    {
        static Func<object>   underlying_callback;
        static Callback callback;
        static System.Type result;

        Establish context = () =>
        {
            underlying_callback = () => "Fourty Two";
            callback = new Callback(underlying_callback);
        };

        Because of = () => result = callback.GetTargetType();

        It should_return_the_type_of_the_target = () => result.ShouldEqual(underlying_callback.GetType());
    }
}