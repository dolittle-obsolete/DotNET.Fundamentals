using System.Linq;
using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingProviderBuilder
{
    public class when_building_a_binding_of_type_to_another_type_with_singleton_scope : given.a_binding_provider_builder
    {
        static Binding result;

        Establish context = () => 
        {
            builder.Bind(typeof(object)).To(typeof(string)).Singleton();
        };

        Because of = () => result = builder.Build().First();

        It should_hold_binding_with_correct_service_type = () => result.Service.ShouldEqual(typeof(object));
        It should_hold_correct_target_type = () => ((Strategies.Type)result.Strategy).Target.ShouldEqual(typeof(string));
        It should_scoped_to_singleton = () => result.Scope.ShouldBeOfExactType<Scopes.Singleton>();
    }
}