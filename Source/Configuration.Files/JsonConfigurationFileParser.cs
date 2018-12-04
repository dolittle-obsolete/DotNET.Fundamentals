/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Dolittle.Types;

namespace Dolittle.Configuration.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanParseConfigurationFile"/> for JSON
    /// </summary>
    public class JsonConfigurationFileParser : ICanParseConfigurationFile
    {
        readonly ISerializationOptions _serializationOptions = SerializationOptions.Custom(callback:
            serializer =>
            {
                serializer.ContractResolver = new CamelCaseExceptDictionaryKeyResolver();
            }
        );

        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="JsonConfigurationFileParser"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/></param>
        /// <param name="container"><see cerf="IContainer"/> used to get instances</param>
        /// <param name="logger"></param>
        public JsonConfigurationFileParser(
            ITypeFinder typeFinder,
            IContainer container,
            ILogger logger)
        {
            var converterInstances = new InstancesOf<ICanProvideConverters>(typeFinder, container);
            _serializer = new Serializer(converterInstances);
            _logger = logger;
        }

        /// <inheritdoc/>
        public bool CanParse(Type type, string filename, string content)
        {
            if( content.StartsWith("{") ) return true;
            return Path.GetExtension(filename).ToLowerInvariant().Equals("json");
        }

        /// <inheritdoc/>
        public object Parse(Type type, string filename, string content)
        {
            return _serializer.FromJson(type, content, _serializationOptions);
        }
    }
}