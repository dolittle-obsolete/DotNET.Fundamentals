using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Execution.for_ExecutionContextManager.given
{
    public class an_execution_context_manager
    {
        protected static ExecutionContextManager execution_context_manager;
        protected static ILogger logger;

        Establish context = () => 
        {
            logger = Mock.Of<ILogger>();
            execution_context_manager = new ExecutionContextManager(logger);
        };
    }
}