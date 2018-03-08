using Machine.Specifications;

namespace Dolittle.DependencyInversion.for_BindingBuilder.given
{
    public class a_null_binding
    {
        protected static Binding binding;
        protected static BindingBuilder builder;

        Establish context = () => 
        {
            binding = new Binding(typeof(object), new Strategies.Null(), new Scopes.Transient());
            builder = new BindingBuilder(binding);
        };
    }
}