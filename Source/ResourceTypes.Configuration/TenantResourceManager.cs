// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Tenancy;
using Dolittle.Types;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantResourceManager : ITenantResourceManager
    {
        readonly IEnumerable<IRepresentAResourceType> _resourceDefinitions;
        readonly ICanProvideResourceConfigurationsByTenant _resourceConfigurationByTenantProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantResourceManager"/> class.
        /// </summary>
        /// <param name="resourceDefinitions"><see cref="IInstancesOf{T}"/> of <see cref="IRepresentAResourceType"/>.</param>
        /// <param name="resourceConfigurationByTenantProvider"><see cref="ICanProvideResourceConfigurationsByTenant"/> for providing configuration for resources.</param>
        public TenantResourceManager(IInstancesOf<IRepresentAResourceType> resourceDefinitions, ICanProvideResourceConfigurationsByTenant resourceConfigurationByTenantProvider)
        {
            _resourceDefinitions = resourceDefinitions;
            _resourceConfigurationByTenantProvider = resourceConfigurationByTenantProvider;
        }

        /// <inheritdoc/>
        public T GetConfigurationFor<T>(TenantId tenantId)
            where T : class
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