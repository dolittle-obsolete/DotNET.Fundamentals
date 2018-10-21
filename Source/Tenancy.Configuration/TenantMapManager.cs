using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Dolittle.Tenancy.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantMapManager : ITenantMapManager
    {
        readonly string _path = Path.Combine(".dolittle", "tenant-map.json");
        const string _strategyJsonKey = "strategy";
        
        readonly IEnumerable<IRepresentATenantStrategy> _tenantStrategyRepresentations;
        readonly ISerializer _serializer;
        readonly TenantStrategy _tenantStrategy;
        readonly string _strategyConfigurationAsString;

        /// <summary>
        /// Instantiates an instance of <see cref="TenantMapManager"/>
        /// </summary>
        /// <param name="instancesOfTenantStrategyRepresentations"></param>
        /// <param name="serializer"></param>
        public TenantMapManager(IInstancesOf<IRepresentATenantStrategy> instancesOfTenantStrategyRepresentations, ISerializer serializer)
        {
            while(!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(20);
            _tenantStrategyRepresentations = instancesOfTenantStrategyRepresentations;
            _serializer = serializer;

            var path = Path.Combine(Directory.GetCurrentDirectory(), _path);
            
            if (File.Exists(path)) 
            {
                var fileContent = File.ReadAllText(path);
                var jsonObject = JObject.Parse(fileContent);
                _tenantStrategy = (string)jsonObject[_strategyJsonKey];
                jsonObject.Remove(_strategyJsonKey);
                _strategyConfigurationAsString = jsonObject.ToString(Formatting.None);
            }
            
        }

        /// <inheritdoc/>
        public TenantStrategy Strategy => _tenantStrategy;

        /// <inheritdoc/>
        public T InstanceOfStrategy<T>() where T : class => (T)InstanceOfStrategy(typeof(T));
        
        /// <inheritdoc/>
        public object InstanceOfStrategy(Type strategyType)
        {
            var instance = _serializer.FromJson(strategyType, _strategyConfigurationAsString);
            if (instance == null) throw new WrongStrategyConfiguration(strategyType);

            return instance;
        }
    }
}