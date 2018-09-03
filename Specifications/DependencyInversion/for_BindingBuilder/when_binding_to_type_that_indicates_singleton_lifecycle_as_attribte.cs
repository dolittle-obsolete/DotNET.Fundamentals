using Dolittle.Lifecycle;
using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingBuilder
{
    public class when_binding_to_type_that_indicates_singleton_lifecycle_as_attribte : given.a_null_binding
    {
        [Singleton]
        public class MyType {}

        static Binding result;

        Because of = () => 
        {
            builder.To(typeof(MyType));
            result = builder.Build();
        };

        It should_have_a_type_strategy = () => result.Strategy.ShouldBeOfExactType<Strategies.Type>();
        It should_hold_the_type_in_the_strategy = () => ((Strategies.Type)result.Strategy).Target.ShouldEqual(typeof(MyType));
        It should_have_singleton_scope = () => result.Scope.ShouldBeAssignableTo<Scopes.Singleton>();
    }
}