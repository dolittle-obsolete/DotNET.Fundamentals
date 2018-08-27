using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Bootstrapping.Specs.for_Bootstrapper.given
{
    public class all_dependencies
    {
        protected static Mock<IContainer> container;
        protected static Mock<ILogger> logger;

        Establish context = () => 
        {
            logger = new Mock<ILogger>();
            container = new Mock<IContainer>();
            container.Setup(_ => _.Get<ILogger>()).Returns(logger.Object);
        };
    }
}