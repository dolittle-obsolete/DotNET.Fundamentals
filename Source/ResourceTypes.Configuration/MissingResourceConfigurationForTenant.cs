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
    /// The exception that gets thrown when resources for a <see cref="TenantId"/> is not found in the resource file.
    /// </summary>
    public class MissingResourceConfigurationForTenant : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="MissingResourceConfigurationForTenant"/>
        /// </summary>
        /// <param name="tenantId">The <see cref="TenantId"/> that has missing resource configuration</param>
        public MissingResourceConfigurationForTenant(
            TenantId tenantId)
            : base($"Tenant with id '{tenantId}' does not have a any resource configurations'")
        { }
    }
}