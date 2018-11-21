/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Serialization.Json;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanParseConfigurationFile"/> for JSON
    /// </summary>
    public class JsonConfigurationFileParser : ICanParseConfigurationFile
    {
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="JsonConfigurationFileParser"/>
        /// </summary>
        /// <param name="serializer">JSON <see cref="ISerializer">serializer</see> to use</param>
        public JsonConfigurationFileParser(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public bool CanParse(string filename, string content)
        {
            if( content.StartsWith("{") ) return true;
            return Path.GetExtension(filename).ToLowerInvariant().Equals("json");
        }

        /// <inheritdoc/>
        public object Parse(Type type, string filename, string content)
        {
            return _serializer.FromJson(type, content);
        }
    }
}