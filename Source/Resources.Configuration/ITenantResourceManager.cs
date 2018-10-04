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