using System;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class ReadModelRepresentation : IRepresentAResourceType
    {
        public ResourceType ResourceType => new ResourceType(){Value = "readModels"};
        
        public Type ConfigurationType => typeof(ReadModelConfiguration);
    }
}