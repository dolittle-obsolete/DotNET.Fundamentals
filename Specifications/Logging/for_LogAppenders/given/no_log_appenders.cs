using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Specs.Logging.for_LogAppenders.given
{
    public class no_log_appenders : all_dependencies
    {
        protected static LogAppenders appenders;

        Establish context = () => appenders = new LogAppenders(log_appenders_configurators.Object);
    }
}
