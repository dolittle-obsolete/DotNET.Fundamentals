using Dolittle.Mapping;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Mapping.Specs.for_Maps.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<IMap>> map_instances_mock;

        Establish context = () => map_instances_mock = new Mock<IInstancesOf<IMap>>();
    }
}
