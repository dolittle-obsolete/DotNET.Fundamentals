using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingBuilder.given
{
    public class a_null_binding_for_generic_builder
    {
        protected static Binding binding;
        protected static BindingBuilder<string> builder;

        Establish context = () => 
        {
            binding = new Binding(typeof(string), new Strategies.Null(), new Scopes.Transient());
            builder = new BindingBuilder<string>(binding);
        };
    }
}