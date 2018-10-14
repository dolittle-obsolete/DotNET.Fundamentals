/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using Dolittle.Types;

namespace Dolittle.Resources.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantResourceManager : ITenantResourceManager
    {
        IEnumerable<IRepresentAResourceType> _resourceDefinitions;
        ICanProvideResourceConfigurationsByTenant _resourceConfigurationByTenantProvider;

        /// <summary>
        /// Instantiates an instance of <see cref="TenantResourceManager"/>
        /// </summary>
        /// <param name="resourceDefinitions"></param>
        /// <param name="resourceConfigurationByTenantProvider"></param>
        public TenantResourceManager(IInstancesOf<IRepresentAResourceType> resourceDefinitions, ICanProvideResourceConfigurationsByTenant resourceConfigurationByTenantProvider)
        {
            _resourceDefinitions = resourceDefinitions;
            _resourceConfigurationByTenantProvider = resourceConfigurationByTenantProvider;
        }
        
        /// <inheritdoc/>
        public T GetConfigurationFor<T>(TenantId tenantId) where T : class
        {
            var resourceType = RetrieveResourceType<T>();
            
            return _resourceConfigurationByTenantProvider.ConfigurationFor<T>(tenantId, resourceType);
        }

        ResourceType RetrieveResourceType<T>()
        {
            var resourceTypesMatchingType = _resourceDefinitions.Where(_ => _.ConfigurationObjectType.Equals(typeof(T))).ToArray();
            var length = resourceTypesMatchingType.Length;
            if (length == 0) throw new NoResourceTypeMatchingConfigurationType(typeof(T));
            if (length > 1) throw new ConfigurationTypeMappedToMultipleResourceTypes(typeof(T));

            return resourceTypesMatchingType[0].Type;
        }
    }
}