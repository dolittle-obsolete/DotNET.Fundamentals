/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// The Exception that gets thrown when trying to find a <see cref="ResourceType"/> mapping to a specific <see cref="Type"/>
    /// </summary>
    public class NoResourceTypeMatchingConfigurationType : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="NoResourceTypeMatchingConfigurationType"/>
        /// </summary>
        /// <param name="type"></param>
        public NoResourceTypeMatchingConfigurationType(Type type)
            : base($"Could not map the Type {type.FullName} up to a {typeof(ResourceType).FullName}")
        {}
    }
}