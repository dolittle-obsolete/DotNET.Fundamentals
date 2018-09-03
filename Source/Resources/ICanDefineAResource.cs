/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// Defines a system that defines a resource
    /// </summary>
    public interface ICanDefineAResource
    {
        /// <summary>
        /// Gets the <see cref="ResourceType"/> it represents
        /// </summary>
        ResourceType    ResourceType { get; }

        /// <summary>
        /// Gets the <see cref="ResourceTypeName"/> of the resource - identified typically in configuration files
        /// </summary>
        ResourceTypeName ResourceTypeName { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the configuration object to expect and create proper bindings for
        /// </summary>
        Type ConfigurationObjectType { get; }
    }
}