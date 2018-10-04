using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class a_representation_of_a_readModels_and_eventStore : a_serializer
    {
        protected static Mock<IInstancesOf<IRepresentAResourceType>> resource_type_representer_instances;
        protected static IEnumerable<IRepresentAResourceType> resource_type_representers;
        Establish context = () => 
        {
            resource_type_representers = new List<IRepresentAResourceType>();
            resource_type_representer_instances = new Mock<IInstancesOf<IRepresentAResourceType>>();
            resource_type_representer_instances.Setup(_ => _.GetEnumerator()).Returns(resource_type_representers.GetEnumerator());
        };
    }
}