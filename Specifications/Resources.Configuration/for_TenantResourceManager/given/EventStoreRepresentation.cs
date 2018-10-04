using System;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class EventStoreRepresentation : IRepresentAResourceType
    {
        public ResourceType Type => new ResourceType(){Value = "eventStore"};
        public Type ConfigurationType => typeof(ReadModelConfiguration);
    }
}