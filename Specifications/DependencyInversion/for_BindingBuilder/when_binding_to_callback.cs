using System;
using Machine.Specifications;

namespace doLittle.DependencyInversion.for_BindingBuilder
{
    public class when_binding_to_callback : given.a_null_binding
    {
        static Binding result;
        static Func<Type, object> callback = (Type type) => "result";

        Because of = () => 
        {
            builder.To(callback);
            result = builder.Build();
        };

        It should_have_a_callback_strategy = () => result.Strategy.ShouldBeOfExactType<Strategies.Callback>();
        It should_hold_the_delegeate_in_the_strategy = () => ((Strategies.Callback)result.Strategy).Target.ShouldEqual(callback);
        It should_have_transient_scope = () => result.Scope.ShouldBeAssignableTo<Scopes.Transient>();
    }
}