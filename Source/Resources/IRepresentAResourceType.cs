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
    public interface IRepresentAResourceType
    {
        /// <summary>
        /// Gets the <see cref="ResourceType"/> it represents
        /// </summary>
        ResourceType Type { get; }

        /// <summary>
        /// Gets the <see cref="ResourceTypeImplementation"/> of the resource - identified typically in configuration files
        /// </summary>
        ResourceTypeImplementation ImplementationName { get; }

        /// <summary>
        /// Gets the <see cref="System.Type"/> of the configuration object to expect and create proper bindings for
        /// </summary>
        Type ConfigurationObjectType { get; }

        /// <summary>
        /// Gets the types that'll be bound at bootstrap
        /// </summary>
        IDictionary<Type, Type> Bindings { get; }
    }
}