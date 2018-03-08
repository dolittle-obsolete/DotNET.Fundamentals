/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Dolittle.Applications;
using Newtonsoft.Json;

namespace Dolittle.Runtime.Applications.Serialization.Json
{
    /// <summary>
    /// Represents a <see cref="JsonConverter"/> that can serialize and deserialize <see cref="IApplicationArtifactIdentifier"/>
    /// </summary>
    public class ApplicationArtifactIdentifierJsonConverter : JsonConverter
    {
        IApplicationArtifactIdentifierStringConverter _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactIdentifierJsonConverter"/>
        /// </summary>
        /// <param name="converter"><see cref="IApplicationArtifactIdentifierStringConverter"/> for converting to and from string representations</param>
        public ApplicationArtifactIdentifierJsonConverter(IApplicationArtifactIdentifierStringConverter converter)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        public override bool CanRead { get { return true; } }

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IApplicationArtifactIdentifier).GetTypeInfo().IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var identifierAsString = reader.Value.ToString();
            var identifier = _converter.FromString(identifierAsString);
            return identifier;
        }

        /// <inheritdoc/>
        public override bool CanWrite { get { return true; } }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var identifier = value as IApplicationArtifactIdentifier;
            if( identifier != null )
            {
                var identifierAsString = _converter.AsString(identifier);
                writer.WriteValue(identifierAsString);
            }
        }
    }
}
