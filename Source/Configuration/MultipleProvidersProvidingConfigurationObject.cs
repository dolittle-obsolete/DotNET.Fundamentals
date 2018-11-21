/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Exception that gets thrown when there are multiple providers providing a specific <see cref="IConfigurationObject"/>
    /// </summary>
    public class MultipleProvidersProvidingConfigurationObject : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleProvidersProvidingConfigurationObject"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IConfigurationObject"/></param>
        public MultipleProvidersProvidingConfigurationObject(Type type) : base($"There are multiple providers that can provide the configuration object '{type.GetFriendlyConfigurationName()}' - '{type.Name}'")
        {

        }
    }
}
