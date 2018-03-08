/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Configuration;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Defines a system that is capable of providing configuration value to a service implementation
    /// for a resource
    /// </summary>
    public interface ICanProvideConfigurationValueToResourceServiceImplementation
    {
        /// <summary>
        /// Checks if <see cref="IConfigurationValue"/> can be provided
        /// </summary>
        /// <param name="owner">Owning resource that is expecting the <see cref="IConfigurationValue"/></param>
        /// <param name="configurationValueType">Type of <see cref="IConfigurationValue"/></param>
        /// <param name="name">Name of the <see cref="IConfigurationValue"/></param>
        /// <returns>True if it can provide, false if not</returns>
        bool CanProvide(Type owner, Type configurationValueType, string name);

        /// <summary>
        /// Provide a <see cref="IConfigurationValue"/> of a given type with a given name
        /// </summary>
        /// <param name="owner">Owning resource that is expecting the <see cref="IConfigurationValue"/></param>
        /// <param name="configurationValueType">Type of <see cref="IConfigurationValue"/></param>
        /// <param name="name">Name of the <see cref="IConfigurationValue"/></param>
        /// <returns>The actual <see cref="IConfigurationValue"/></returns>
        IConfigurationValue Provide(Type owner, Type configurationValueType, string name);
    }
}