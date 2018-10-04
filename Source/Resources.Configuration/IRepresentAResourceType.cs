using System;

namespace Dolittle.Resources.Configuration
{

    /// Defines a system for mapping Configurations to <see cref="ResourceType"/>
    public interface IRepresentAResourceType
    {
        /// <summary>
        /// Gets the <see cref="ResourceType"/>
        /// </summary>
         ResourceType Type { get; }
        
        /// <summary>
        /// Gets the <see cref="Type"/> of the Configuration
        /// </summary>
         Type ConfigurationType { get; }
    }
}