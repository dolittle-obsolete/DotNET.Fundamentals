using Machine.Specifications;

namespace Dolittle.Execution.for_ExecutionContextManager.given
{
    public class an_execution_context_manager
    {
        protected static ExecutionContextManager execution_context_manager;

        Establish context = () => execution_context_manager = new ExecutionContextManager();
    }
}