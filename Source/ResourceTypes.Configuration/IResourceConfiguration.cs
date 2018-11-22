/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Represents a configuration for the Resource System
    /// </summary>
    public interface IResourceConfiguration
    {
        /// <summary>
        /// Gets whether or not the resource configuration system is configured
        /// </summary>
        /// <value></value>
        bool IsConfigured {get;}

        /// <summary>
        /// Gets the implementation for a specific service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Type GetImplementationFor(Type service);

        /// <summary>
        /// Sets the ResourceType to ResourceTypeImplementation mapping 
        /// </summary>
        /// <param name="resourceTypeToImplementationMap"></param>
        void ConfigureResourceTypes(IDictionary<ResourceType, ResourceTypeImplementation> resourceTypeToImplementationMap);      
    }
}