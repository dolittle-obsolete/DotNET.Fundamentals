/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class ResourceConfigurationsByTenantProvider : ICanProvideResourceConfigurationsByTenant
    {
        static readonly string _path = Path.Combine(".dolittle", "resources.json");
        
        IDictionary<TenantId, Dictionary<ResourceType, object>> _resourceConfigurationsByTenant = new Dictionary<TenantId, Dictionary<ResourceType, object>>();
        readonly ISerializer _serializer;

        /// <summary>
        /// Instantiates an instance of <see cref="ResourceConfigurationsByTenantProvider"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> for serializing</param>
        public ResourceConfigurationsByTenantProvider(
            ISerializer serializer)
        {
            _serializer = serializer;

            HandleResourceFile();
        }

        /// <inheritdoc/>
        public object ConfigurationFor(Type configurationType, TenantId tenantId, ResourceType resourceType)
        {
            var configurationObject = GetResourceConfiguration(tenantId, resourceType);
            if( configurationObject is string )
                configurationObject = _serializer.FromJson(configurationType, configurationObject as string); 

            return configurationObject;
        }

        /// <inheritdoc/>
        public T ConfigurationFor<T>(TenantId tenantId, ResourceType resourceType)
        {
            return (T)ConfigurationFor(typeof(T), tenantId, resourceType);
        }


        /// <inheritdoc/>
        public void AddConfigurationFor(TenantId tenantId, ResourceType resourceType, object configurationObject)
        {
            var configurationObjects = GetConfigurationObjectsFor(tenantId);
            ThrowIfTenantAlreadyHasConfigurationForResourceType(tenantId, resourceType, configurationObjects);
        }

        Dictionary<ResourceType, object> GetConfigurationObjectsFor(TenantId tenantId)
        {
            Dictionary<ResourceType, object> configurationObjects = null;

            if( _resourceConfigurationsByTenant.ContainsKey(tenantId))
                configurationObjects = _resourceConfigurationsByTenant[tenantId];

            if (configurationObjects == null)
            {
                configurationObjects = new Dictionary<ResourceType, object>();
                _resourceConfigurationsByTenant[tenantId] = configurationObjects;
            }

            return configurationObjects;
        }

        void ThrowIfTenantAlreadyHasConfigurationForResourceType(TenantId tenantId, ResourceType resourceType, Dictionary<ResourceType, object> configurationObjects)
        {
            if (configurationObjects.ContainsKey(resourceType)) throw new ResourceAlreadyHasConfigurationForTenant(tenantId, resourceType);
        }

        void HandleResourceFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _path);
            if (! File.Exists(path)) return;
            var content = File.ReadAllText(path);

            _resourceConfigurationsByTenant = _serializer.FromJson<Dictionary<TenantId, Dictionary<ResourceType, object>>>(content);
        }

        object GetResourceConfiguration(TenantId tenantId, ResourceType resourceType)
        {
            if (!_resourceConfigurationsByTenant.ContainsKey(tenantId)) throw new TenantIdNotPresentInResourceFile(tenantId);
            var configurationByResourceType = _resourceConfigurationsByTenant[tenantId];
            if (! configurationByResourceType.ContainsKey(resourceType)) throw new ResourceTypeNotFoundInResourceFile(tenantId, resourceType);

            return configurationByResourceType[resourceType];
        }
    }
}