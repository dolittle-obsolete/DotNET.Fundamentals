using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_BoundedContext
{
    public class when_adding_existing_module
    {
        static BoundedContext bounded_context;
        static Module module;
        static Exception exception;

        Establish context = () =>
        {
            bounded_context = new BoundedContext(new BoundedContextName{Value = "Some bounded context"});
            module = new Module(bounded_context, "Module");
        };

        Because of = () => exception = Catch.Exception(() => bounded_context.AddModule(module));

        It should_throw_module_already_added_to_bounded_context = () => exception.ShouldBeOfExactType<ModuleAlreadyAddedToBoundedContext>();
    }
}
