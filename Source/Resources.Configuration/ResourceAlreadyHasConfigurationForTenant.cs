/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ResourceType"/> already has configuration set for a <see cref="TenantId"/>
    /// </summary>
    public class ResourceAlreadyHasConfigurationForTenant : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceAlreadyHasConfigurationForTenant"/>
        /// </summary>
        /// <param name="tenant"><see cref="TenantId">Tenant</see> that already has the configuration set</param>
        /// <param name="resourceType"><see cref="ResourceType"/> that already has the configuration set</param>
        public ResourceAlreadyHasConfigurationForTenant(TenantId tenant, ResourceType resourceType) 
            : base($"There already is a configuration for '{resourceType}' for tenant with Id '{tenant}'") {}
    }
}