using Dolittle.Logging;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Dolittle.Bootstrapping.Specs.for_Bootstrapper
{
    public class when_starting_with_two_procedures : given.two_procedures
    {
        Because of = () => Bootstrapper.Start(container.Object, Mock.Of<ILogger>());

        It should_perform_first_procedure = () => first_procedure.Verify(_ => _.Perform(), Moq.Times.Once());
        It should_perform_second_procedure = () => second_procedure.Verify(_ => _.Perform(), Moq.Times.Once());
    }
}