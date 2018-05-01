/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Defines a converter that is capable of converting to and from 
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Determines whether this <see cref="IValueConverter"/> can convert the specified object type.
        /// </summary>
        /// <param name="objectType"><see cref="Type"/> of object </param>
        /// <returns>True if it can convert, false if not</returns>
        bool CanConvert(Type objectType);

        /// <summary>
        /// Gets the type used as serialized version
        /// </summary>
        Type SerializedAs { get; }

        /// <summary>
        /// Convert to the value that will be used for serializing
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Converted value</returns>
        object ConvertTo(object value);

        /// <summary>
        /// Convert to the target value on an object from a serialized version of it
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Converted value</returns>
        object ConvertFrom(object value);
    }
}
