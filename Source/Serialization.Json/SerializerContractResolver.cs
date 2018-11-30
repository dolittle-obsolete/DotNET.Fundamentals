/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Strings;
using Dolittle.Reflection;
using Dolittle.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Serialization.Json
{
    /// <summary>
    /// Represents a <see cref="IContractResolver"/> based on the <see cref="DefaultContractResolver"/> for resolving contracts for serialization
    /// </summary>
    public class SerializerContractResolver : DefaultContractResolver
    {
        readonly ISerializationOptions _options;
        readonly bool _ignoreReadOnlyProperties;

        /// <summary>
        /// Initializes a new instance of <see cref="SerializerContractResolver"/>
        /// </summary>
        /// <param name="options"><see cref="ISerializationOptions"/> to use during resolving</param>
        /// <param name="ignoreReadOnlyProperties">Wether or not to ignore read only properties - default false</param>       
        public SerializerContractResolver(ISerializationOptions options, bool ignoreReadOnlyProperties = false)
        {
            _options = options;
            _ignoreReadOnlyProperties = ignoreReadOnlyProperties;
        }


        /// <inheritdoc/>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            if( _ignoreReadOnlyProperties ) 
                properties = properties.Where(p => p.Writable == true).ToList();

            if (_options != null)
                return properties.Where(p => _options.ShouldSerializeProperty(type, p.PropertyName)).ToList();

            return properties;
        }

        /// <inheritdoc/>
        protected override string ResolvePropertyName(string propertyName)
        {
            var result = base.ResolvePropertyName(propertyName);
            if (_options != null &&
                _options.Flags.HasFlag(SerializationOptionsFlags.UseCamelCase))
                result = result.ToCamelCase();

            return result;
        }

        /// <inheritdoc />
        protected override JsonObjectContract CreateObjectContract(Type type)
        {
            var contract = base.CreateObjectContract(type);
            if (type.HasDefaultConstructor()) 
                return contract;

            var ctor = type.GetNonDefaultConstructorWithGreatestNumberOfParameters();
            if (ctor != null)
            {
                contract.OverrideCreator = CreateObjectConstructorFrom(ctor);
                var ctorParams = CreateConstructorParameters(ctor, contract.Properties); 
                ctorParams.ForEach(cp => AddConstructorParameterIfNotAlreadyAdded(contract, cp));
            }

            return contract;
        }

        void AddConstructorParameterIfNotAlreadyAdded(JsonObjectContract contract, JsonProperty property)
        {
            if(ConstructorParameterAlreadyExists(contract,property))
                return;

            contract.CreatorParameters.Add(property);
        }

        bool ConstructorParameterAlreadyExists(JsonObjectContract contract, JsonProperty property)
        {
            return contract.CreatorParameters.Any(p => p.PropertyName == property.PropertyName && p.PropertyType == property.PropertyType);
        }

        ObjectConstructor<object> CreateObjectConstructorFrom(ConstructorInfo ctorInfo)
        {
            if(ctorInfo == null)
                throw new ArgumentNullException(nameof(ctorInfo));
            return a => ctorInfo.Invoke(a);
        }
    }
}