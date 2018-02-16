using Machine.Specifications;

namespace doLittle.DependencyInversion.for_BindingBuilder.given
{
    public class a_null_binding_for_generic_builder
    {
        protected static Binding binding;
        protected static BindingBuilder<object> builder;

        Establish context = () => 
        {
            binding = new Binding(typeof(object), new Strategies.Null(), new Scopes.Transient());
            builder = new BindingBuilder<object>(binding);
        };
    }
}