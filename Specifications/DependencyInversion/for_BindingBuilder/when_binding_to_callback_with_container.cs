using System;
using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingBuilder
{
    public class when_binding_to_callback_with_container: given.a_null_binding
    {
        static Binding result;
        static Func<IContainer, object> callback = (IContainer container) => "result";

        Because of = () => 
        {
            builder.To(callback);
            result = builder.Build();
        };

        It should_have_a_callback_strategy = () => result.Strategy.ShouldBeOfExactType<Strategies.CallbackWithContainer>();
        It should_hold_the_delegeate_in_the_strategy = () => ((Strategies.CallbackWithContainer)result.Strategy).Target.ShouldEqual(callback);
        It should_have_transient_scope = () => result.Scope.ShouldBeAssignableTo<Scopes.Transient>();
    }
}