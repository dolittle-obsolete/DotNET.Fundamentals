/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Defines a system for managing the resources of a Tenant
    /// </summary>
    public interface ITenantResourceManager
    {
        /// <summary>
        /// Gets a configuration 
        /// </summary>
        T GetConfigurationFor<T>(TenantId tenantId) where T : class;
    }
}