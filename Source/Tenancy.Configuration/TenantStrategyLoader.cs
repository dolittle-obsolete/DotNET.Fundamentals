/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dolittle.Tenancy.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantStrategyLoader : ITenantStrategyLoader
    {
        const string _strategyJsonKey = "strategy";
        readonly string _path = Path.Combine(".dolittle", "tenant-map.json");
        readonly ISerializer _serializer;
        readonly string _strategyConfigurationAsString;
        
        /// <summary>
        /// Instantiates an instance of <see cref="TenantStrategyLoader"/>
        /// </summary>
        /// <param name="serializer"></param>
        public TenantStrategyLoader(ISerializer serializer)
        {
            _serializer = serializer;

            var path = Path.Combine(Directory.GetCurrentDirectory(), _path);
            
            if (File.Exists(path)) 
            {
                var fileContent = File.ReadAllText(path);
                var jsonObject = JObject.Parse(fileContent);
                Strategy = (string)jsonObject[_strategyJsonKey];
                jsonObject.Remove(_strategyJsonKey);
                _strategyConfigurationAsString = jsonObject.ToString(Formatting.None);
            }
            
        }
        /// <inheritdoc/>    
        public TenantStrategy Strategy {get;}
        /// <inheritdoc/>
        public object GetStrategyInstance(Type strategyType) => _serializer.FromJson(strategyType, _strategyConfigurationAsString);
    }
}