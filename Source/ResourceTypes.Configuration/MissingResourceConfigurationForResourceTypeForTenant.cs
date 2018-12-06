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
    public class MissingResourceConfigurationForResourceTypeForTenant : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="MissingResourceConfigurationForResourceTypeForTenant"/>
        /// </summary>
        /// <param name="tenantId">The <see cref="TenantId"/> the configuration is missing for</param>
        /// <param name="resourceType">The <see cref="ResourceType"/> that's missing</param>
        public MissingResourceConfigurationForResourceTypeForTenant(TenantId tenantId, ResourceType resourceType)
            : base($"Missing resource configuration for resource typeof {resourceType} for tenant with Id '{tenantId}'")
        { }
    }
}