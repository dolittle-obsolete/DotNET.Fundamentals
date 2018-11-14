/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// The exception that gets thrown when the <see cref="ResourceConfiguration"/> is configured twice
    /// </summary>
    public class ResourceConfigurationAlreadyConfigured : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ResourceConfigurationAlreadyConfigured"/>
        /// </summary>
        public ResourceConfigurationAlreadyConfigured() : base("Resource configuration has already been configured")
        {
        }
    }
}