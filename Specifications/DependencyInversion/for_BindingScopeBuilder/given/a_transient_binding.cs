using Machine.Specifications;
using Moq;

namespace Dolittle.DependencyInversion.for_BindingScopeBuilder.given
{
    public class a_transient_binding
    {
        protected static Binding binding;
        protected static BindingScopeBuilder builder;
        protected static Mock<IActivationStrategy> activation_strategy;

        Establish context = () => 
        {
            activation_strategy = new Mock<IActivationStrategy>();
            activation_strategy.Setup(_ => _.GetTargetType()).Returns(typeof(object));
            binding = new Binding(typeof(object), activation_strategy.Object, new Scopes.Transient());
            builder = new BindingScopeBuilder(binding);
        };
    }
}