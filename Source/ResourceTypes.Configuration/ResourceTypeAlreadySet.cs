/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when a ResourceType has already been mapped up to a ResourceTypeImplementation
    /// </summary>
    public class ResourceTypeAlreadySet : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ResourceTypeAlreadySet"/>
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceTypeImplementation"></param>
        public ResourceTypeAlreadySet(ResourceType resourceType, ResourceTypeImplementation resourceTypeImplementation)
            : base($"")
        { }

    }
}