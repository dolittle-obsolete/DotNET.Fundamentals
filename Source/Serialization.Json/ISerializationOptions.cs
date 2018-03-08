/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

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
    }
}