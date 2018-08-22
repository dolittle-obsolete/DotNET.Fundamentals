/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dolittle.Concepts.Serialization.Json
{
    /// <summary>
    /// Represents a <see cref="JsonConverter"/> that can serialize and deserialize a <see cref="IDictionary{TKey, TValue}">dictionary</see> of <see cref="ConceptAs{T}"/>
    /// </summary>
    public class ConceptDictionaryConverter : JsonConverter, IRequireSerializer
    {
        ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Instantiates an instance of the <see cref="ConceptDictionaryConverter" />
        /// </summary>
        /// <param name="logger">For logging</param>
        public ConceptDictionaryConverter(ILogger logger = null)
        {
            _logger = logger;
        }
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            if (objectType.HasInterface(typeof(IDictionary<,>)) && objectType.GetTypeInfo().IsGenericType ) 
            {
                var keyType = objectType.GetTypeInfo().GetGenericArguments()[0].GetTypeInfo().BaseType;
                return keyType.IsConcept();
            }

            return false;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var keyType = objectType.GetTypeInfo().GetGenericArguments()[0];
            var keyValueType = keyType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
            var valueType = objectType.GetTypeInfo().GetGenericArguments()[1];
            var dictionary = new Dictionary<object,object>();
            JObject jsonDictionary = JObject.Load(reader);
            foreach(var entry in jsonDictionary.Properties())
            { 
                try
                {
                    var kvp = BuildKeyValuePair(entry,keyType,valueType);
                    dictionary.Add(kvp.Key,kvp.Value);
                } 
                catch(Exception ex)
                {

                    if (_logger != null) _logger.Error($"Error reading json: {ex.Message}");
                    throw ex;
                }
            }
            try
            {
                var finalDictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType,valueType);
                var finalDictionary = (IDictionary)Activator.CreateInstance(finalDictionaryType);
                foreach(var key in dictionary.Keys)
                {
                    finalDictionary[key] = dictionary[key];
                }

                return finalDictionary;
            } 
            catch (Exception ex)
            {
                if (_logger != null) _logger.Error($"Error reading json: {ex.Message}");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType();
            IEnumerable keys = (IEnumerable)type.GetProperty("Keys").GetValue(value, null);
            IEnumerable values = (IEnumerable)type.GetProperty("Values").GetValue(value, null);
            IEnumerator valueEnumerator = values.GetEnumerator();

            writer.WriteStartObject();
            foreach (object key in keys)
            {
                valueEnumerator.MoveNext();

                writer.WritePropertyName(key.ToString());
                serializer.Serialize(writer, valueEnumerator.Current);
                
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// Adds a <see cref="ISerializer"/> for use when reading json
        /// </summary>
        /// <param name="serializer">Serializer to use to deserialize complex types</param>
        public void Add(ISerializer serializer)
        {
            _serializer = serializer;
        }

        KeyValuePair<object,object> BuildKeyValuePair(JProperty prop, Type keyType, Type valueType)
        {
            var key = ConceptFactory.CreateConceptInstance(keyType, prop.Name);
            
            var value = valueType == typeof(object) ? prop.First() : _serializer.FromJson(valueType, prop.Value.ToString());
            return new KeyValuePair<object,object>(key,value);
        }
    }
}
