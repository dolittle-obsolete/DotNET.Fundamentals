/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;
using Dolittle.Tenancy;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when resources for a <see cref="TenantId"/> of a given <see cref="ResourceType"/> is not found in the resource file.
    /// </summary>
    public class ResourceTypeNotFoundInResourceFile : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ResourceTypeNotFoundInResourceFile"/>
        /// </summary>
        /// <param name="tenantId">The <see cref="TenantId"/> the configuration should be for</param>
        /// <param name="resourceType">The <see cref="ResourceType"/> that's not found in the resource file</param>
        public ResourceTypeNotFoundInResourceFile(TenantId tenantId, ResourceType resourceType)
            : base($"Expected a resource of {typeof(ResourceType).FullName}: {resourceType.Value} under a tenant with {typeof(TenantId).FullName}: '{tenantId.Value.ToString()}', but {resourceType} configuration was not found in the resource file.")
        { }
    }
}