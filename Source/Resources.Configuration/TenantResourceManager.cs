/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Dolittle.Tenancy;
using Dolittle.Types;
using Newtonsoft.Json;

namespace Dolittle.Resources.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantResourceManager : ITenantResourceManager
    {
        static readonly string _path = Path.Combine(".dolittle", "resources.json");

        IInstancesOf<IRepresentAResourceType> _resourceDefinitions;
        ISerializer _serializer;
        ILogger _logger;
        
        IDictionary<TenantId, Dictionary<ResourceType, object>> _resourceConfigurationsByTenant;
        

        /// <summary>
        /// Instantiates an instance of <see cref="TenantResourceManager"/>
        /// </summary>
        /// <param name="resourceDefinitions"></param>
        /// <param name="serializer"></param>
        /// <param name="logger"></param>
        public TenantResourceManager(IInstancesOf<IRepresentAResourceType> resourceDefinitions, ISerializer serializer, ILogger logger)
        {
            _resourceDefinitions = resourceDefinitions;
            _serializer = serializer;
            _logger = logger;

            var resourceFileContent = ReadResourceFile();
            _resourceConfigurationsByTenant = _serializer.FromJson<Dictionary<TenantId, Dictionary<ResourceType, object>>>(resourceFileContent);
        }
        
        /// <inheritdoc/>
        public T GetConfigurationFor<T>(TenantId tenantId) where T : class
        {
            var resourceType = RetrieveResourceType<T>();
            
            var configurationObjectAsString = GetResourceConfigurationJson(tenantId, resourceType);
            var configurationObject = _serializer.FromJson<T>(configurationObjectAsString); 

            return configurationObject;
        }

        ResourceType RetrieveResourceType<T>()
        {
            var resourceTypesMatchingType = _resourceDefinitions.Where(_ => _.ConfigurationObjectType.Equals(typeof(T)));
            if (!resourceTypesMatchingType.Any()) throw new NoResourceTypeMatchingConfigurationType(typeof(T));
            if (resourceTypesMatchingType.Count() > 1) throw new ConfigurationTypeMappedToMultipleResourceTypes(typeof(T));

            return resourceTypesMatchingType.First().Type;
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