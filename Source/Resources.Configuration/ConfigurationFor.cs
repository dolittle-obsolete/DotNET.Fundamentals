/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Execution;
using Dolittle.Lifecycle;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <inheritdoc/>
    [SingletonPerTenant]
    public class ConfigurationFor<T> : IConfigurationFor<T> 
        where T : class
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ConfigurationFor{T}"/>
        /// </summary>
        /// <param name="tenantResourceManager"></param>
        /// <param name="executionContextManager"></param>
        public ConfigurationFor(ITenantResourceManager tenantResourceManager, IExecutionContextManager executionContextManager) 
        {
            Instance = tenantResourceManager.GetConfigurationFor<T>(executionContextManager.Current.Tenant);
               
        }
        /// <inheritdoc/>
        public T Instance {get; }
    }
}