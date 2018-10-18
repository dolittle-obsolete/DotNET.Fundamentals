/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class DolittleResourceConfigurationsByTenantProvider : ICanProvideResourceConfigurationsByTenant
    {
        static readonly string _path = Path.Combine(".dolittle", "resources.json");
        
        readonly IDictionary<TenantId, Dictionary<ResourceType, object>> _resourceConfigurationsByTenant;
        readonly ISerializer _serializer;
        /// <summary>
        /// Instantiates an instance of <see cref="DolittleResourceConfigurationsByTenantProvider"/>
        /// </summary>
        /// <param name="serializer"></param>
        public DolittleResourceConfigurationsByTenantProvider(ISerializer serializer)
        {
            _serializer = serializer;

            var resourceFileContent = ReadResourceFile();
            _resourceConfigurationsByTenant = _serializer.FromJson<Dictionary<TenantId, Dictionary<ResourceType, object>>>(resourceFileContent);

        }
        /// <inheritdoc/>
        public object ConfigurationFor(Type configurationType, TenantId tenantId, ResourceType resourceType)
        {
            var configurationObjectAsString = GetResourceConfigurationJson(tenantId, resourceType);
            var configurationObject = _serializer.FromJson(configurationType, configurationObjectAsString); 

            return configurationObject;
        }

        /// <inheritdoc/>
        public T ConfigurationFor<T>(TenantId tenantId, ResourceType resourceType)
        {
            return (T)ConfigurationFor(typeof(T), tenantId, resourceType);
        }

        string ReadResourceFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _path);
            if (! File.Exists(path)) throw new MissingResourcesFile();
            return File.ReadAllText(path);
        }

        string GetResourceConfigurationJson(TenantId tenantId, ResourceType resourceType)
        {
            if (!_resourceConfigurationsByTenant.ContainsKey(tenantId)) throw new TenantIdNotPresentInResourceFile(tenantId);
            var configurationByResourceType = _resourceConfigurationsByTenant[tenantId];
            if (! configurationByResourceType.ContainsKey(resourceType)) throw new ResourceTypeNotFoundInResourceFile(tenantId, resourceType);

            return configurationByResourceType[resourceType].ToString();
        }

    }
}