/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Exception that gets thrown when there are multiple implementations of <see cref="ICanProvideDefaultConfigurationFor{T}"/>
    /// for a specific <see cref="IConfigurationObject"/>
    /// </summary>
    public class MultipleDefaultConfigurationProvidersFoundForConfigurationObject : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleDefaultConfigurationProvidersFoundForConfigurationObject"/>
        /// </summary>
        /// <param name="type">Type of <see cref="IConfigurationObject"/> there are multiple default providers for</param>
        public MultipleDefaultConfigurationProvidersFoundForConfigurationObject(Type type) : base($"There are multiple implementations of default value providers for configuration object of type '{type.AssemblyQualifiedName}'") {}

    }
}