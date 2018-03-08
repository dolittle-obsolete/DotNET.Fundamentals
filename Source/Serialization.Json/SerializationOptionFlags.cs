/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Serialization.Json
{
    /// <summary>
    /// Represents the flag options for serialization
    /// </summary>
    [Flags]
    public enum SerializationOptionsFlags
    {
        /// <summary>
        /// No specific options
        /// </summary>
        None = 0,

        /// <summary>
        /// Use camel case for naming of properties
        /// </summary>
        UseCamelCase = 1,

        /// <summary>
        /// Include type names during serialization
        /// </summary>
        IncludeTypeNames = 2,
    }
}