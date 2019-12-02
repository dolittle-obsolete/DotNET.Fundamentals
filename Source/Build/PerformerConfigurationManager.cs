// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.IO;
using Dolittle.Lifecycle;
using Dolittle.Serialization.Json;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IPerformerConfigurationManager"/>
    /// </summary>
    [Singleton]
    public class PerformerConfigurationManager : IPerformerConfigurationManager
    {
        private readonly IFileSystem _fileSystem;
        private readonly ISerializer _serializer;

        private IDictionary<string, object> _configObjects;
        private readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of <see cref="PerformerConfigurationManager"/>
        /// </summary>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting messages</param>
        /// <param name="fileSystem"><see cref="IFileSystem"/> to use</param>
        /// <param name="serializer">JSON <see cref="ISerializer"/></param>
        public PerformerConfigurationManager(IBuildMessages buildMessages, IFileSystem fileSystem, ISerializer serializer)
        {
            _fileSystem = fileSystem;
            _serializer = serializer;
            _buildMessages = buildMessages;
        }

        /// <inheritdoc/>
        public void Initialize(string jsonFile)
        {
            var json = string.Empty;

            try
            {
                _buildMessages.Information($"Initializing from file '${jsonFile}'");
                json = _fileSystem.ReadAllText(jsonFile);
                _configObjects = _serializer.GetKeyValuesFromJson(json);
            }
            catch (Exception ex)
            {
                _buildMessages.Error($"Error when initializing '{jsonFile}', content: '{json}'");
                throw ex;
            }
        }

        /// <inheritdoc/>
        public object GetFor(Type configurationType, string name)
        {
            var configObject = _configObjects[name];
            var json = _serializer.ToJson(configObject);
            var instance = _serializer.FromJson(configurationType, json);
            return instance;
        }
    }
}