using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingProviderBuilder
{
    public class when_binding_to_type : given.a_binding_provider_builder
    {
        static Binding result;
        Because of = () => result = builder.Bind(typeof(string)).Build();
        It should_hold_the_service_type_specified = () => result.Service.ShouldEqual(typeof(string));
    }
}