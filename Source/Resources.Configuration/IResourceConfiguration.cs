/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Represents a configuration for the Resource System
    /// </summary>
    public interface IResourceConfiguration
    {
        /// <summary>
        /// Gets the implementation for a specific service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Type GetImplementationFor(Type service);
        /// <summary>
        /// Sets the ResourceType to ResourceTypeImplementation mapping 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceTypeImplementation"></param>
        void SetResourceType(ResourceType resourceType, ResourceTypeImplementation resourceTypeImplementation);
        
    }
}