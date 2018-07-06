using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_BoundedContext
{
    public class when_adding_module
    {
        static BoundedContext bounded_context;
        static Mock<IModule> module;

        Establish context = () =>
        {
            bounded_context = new BoundedContext(new BoundedContextName{Value = "Some bounded context"});
            module = new Mock<IModule>();
        };

        Because of = () => bounded_context.AddModule(module.Object);

        It should_add_the_module = () => bounded_context.Children.ShouldContain(module.Object);
    }
}
