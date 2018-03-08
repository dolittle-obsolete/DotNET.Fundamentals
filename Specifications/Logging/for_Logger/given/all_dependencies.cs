using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Specs.Logging.for_Logger.given
{
    public class all_dependencies
    {
        protected static Mock<ILogAppenders> appenders;

        Establish context = () => appenders = new Mock<ILogAppenders>();
    }
}
