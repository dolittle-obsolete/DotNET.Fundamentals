/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Tenancy;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Defines a system that can provide the <see cref="ITenantResourceManager"/> with resource configurations mapped by <see cref="TenantId"/>
    /// </summary>
    public interface ICanProvideResourceConfigurationsByTenant
    {
        /// <summary>
        /// Gets a specific configuration based on the <see cref="TenantId"/> and the <see cref="ResourceType"/> 
        /// </summary>
        /// <param name="configurationType">The <see cref="Type"/> of the configuration</param>
        /// <param name="tenantId">The <see cref="TenantId"/> that the configuration belongs under</param>
        /// <param name="resourceType">The <see cref="ResourceType"/> that the configuration belongs under</param>
        /// <returns></returns>
        object ConfigurationFor(Type configurationType, TenantId tenantId, ResourceType resourceType);

        /// <summary>
        /// Gets a specific configuration based on the <see cref="TenantId"/> and the <see cref="ResourceType"/> 
        /// </summary>
        /// <param name="tenantId">The <see cref="TenantId"/> that the configuration belongs under</param>
        /// <param name="resourceType">The <see cref="ResourceType"/> that the configuration belongs under</param>
        /// <typeparam name="T">The type of configuration object</typeparam>
        /// <returns></returns>
        T ConfigurationFor<T>(TenantId tenantId, ResourceType resourceType);

        /// <summary>
        /// Add a configuration for a <see cref="TenantId"/> and specific <see cref="ResourceType"/>
        /// </summary>
        /// <param name="tenantId"><see cref="TenantId"/> to add for</param>
        /// <param name="resourceType"><see cref="ResourceType"/> to add for</param>
        /// <param name="configurationObject">Specific configuration object the <see cref="ResourceType"/> is expecting</param>
        void AddConfigurationFor(TenantId tenantId, ResourceType resourceType, object configurationObject);
    }
}