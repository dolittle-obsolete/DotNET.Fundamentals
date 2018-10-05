using System.Collections.Generic;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Represents a resource configuration
    /// </summary>
    public class ResourceConfiguration
    {
        /// <summary>
        /// Gets the resource configuration
        /// </summary>
        public IDictionary<ResourceType, object> Resources {get; set; } = new Dictionary<ResourceType, object>();
    }
}