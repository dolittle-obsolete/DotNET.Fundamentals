/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="serializer"></param>
        public PerformerConfigurationManager(IFileSystem fileSystem, ISerializer serializer)
        {
            _fileSystem = fileSystem;
            _serializer = serializer;
        }
        

        /// <inheritdoc/>
        public void Initialize(string jsonFile)
        {
            var json = _fileSystem.ReadAllText(jsonFile);
            _configObjects = _serializer.GetKeyValuesFromJson(json);
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