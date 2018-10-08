/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// The exception that gets thrown when a <see cref="Type"/> is mapped up to multiple <see cref="ResourceType"/>
    /// </summary>
    public class ConfigurationTypeMappedToMultipleResourceTypes : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ConfigurationTypeMappedToMultipleResourceTypes"/>
        /// </summary>
        /// <param name="type"></param>
        public ConfigurationTypeMappedToMultipleResourceTypes(Type type)
            : base($"The type {type.FullName} is mapped up to multiple Resource Types")
        {}

    }
}