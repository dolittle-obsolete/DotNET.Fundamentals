/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolittle.Serialization.Json
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
    public interface ISerializationOptions
    {
        /// <summary>
        /// Gets whether a property on the given type should be serialized
        /// </summary>
        bool ShouldSerializeProperty(Type type, string propertyName);

        /// <summary>
        /// Gets the flag used for serialization
        /// </summary>
        SerializationOptionsFlags Flags { get; }

        /// <summary>
        /// Gets additional <see cref="JsonConverter">converters</see>
        /// </summary>
        IEnumerable<JsonConverter> Converters { get; }

        /// <summary>
        /// Gets wether or not to ignore discovered converters
        /// </summary>
        bool IgnoreDiscoveredConverters { get; }

        /// <summary>
        /// Gets the callback that can be used to work directly with the <see cref="JsonSerializer">Newtonsoft serializer</see>
        /// </summary>
        Action<JsonSerializer>  Callback { get; }
    }
}