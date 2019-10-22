using System;
using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingBuilder
{
    public class when_binding_to_type_callback_using_generic_service : given.a_null_binding_for_generic_builder
    {
        static Type return_value = typeof(string);
        static Binding result;
        static Func<Type> callback = () => return_value;

        Because of = () => 
        {
            builder.To(callback);
            result = builder.Build();
        };

        It should_have_a_type_callback_strategy = () => result.Strategy.ShouldBeOfExactType<Strategies.TypeCallback>();
        It should_forward_to_given_callback_when_called = () => ((Strategies.TypeCallback)result.Strategy).Target().ShouldEqual(return_value);
        It should_have_transient_scope = () => result.Scope.ShouldBeAssignableTo<Scopes.Transient>();
    }
}