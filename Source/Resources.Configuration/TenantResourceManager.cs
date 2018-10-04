using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Lifecycle;
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
        const string _path = ".dolittle/resources.json";
        // readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
        //     serializer =>
        //     {
        //         serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
        //     }
        // );
        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
            });
        IInstancesOf<IRepresentAResourceType> _resourceTypeRepresentations;
        ISerializer _serializer;

        
        IDictionary<TenantId, ResourceConfiguration> _resourceConfigurationsByTenant {get; }
        

        /// <summary>
        /// Instantiates an instance of <see cref="TenantResourceManager"/>
        /// </summary>
        /// <param name="resourceTypeRepresentations"></param>
        /// <param name="serializer"></param>
        public TenantResourceManager(IInstancesOf<IRepresentAResourceType> resourceTypeRepresentations, ISerializer serializer)
        {
            _resourceTypeRepresentations = resourceTypeRepresentations;
            _serializer = serializer;
            var resourceFileContent = ReadResourceFile();

            _resourceConfigurationsByTenant = _serializer.FromJson<Dictionary<TenantId, ResourceConfiguration>>(resourceFileContent, _serializationOptions);
        }
        /// <inheritdoc/>
        public T GetConfigurationFor<T>(TenantId tenantId) where T : class
        {
            var resourceType = RetrieveResourceType<T>();

            return (T)_resourceConfigurationsByTenant[tenantId].Resources[resourceType];
        }

        ResourceType RetrieveResourceType<T>()
        {
            var resourceTypesMatchingType = _resourceTypeRepresentations.Where(_ => _.Type.Equals(typeof(T)));
            if (!resourceTypesMatchingType.Any()) throw new NoResourceTypeMatchingConfigurationType(typeof(T));
            if (resourceTypesMatchingType.Count() > 1) throw new ConfigurationTypeMappedToMultipleResourceTypes(typeof(T));
            return resourceTypesMatchingType.First().Type;

        }

        string ReadResourceFile()
        {
            var path = GetPath();
            if (! File.Exists(path)) throw new MissingResourcesFile();
            return File.ReadAllText(path);
        }
        string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _path);
        }
    }
}