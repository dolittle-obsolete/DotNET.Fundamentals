/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="IConfigurationObject"/> is unresolved and can't be provided
    /// </summary>
    public class MissingProviderForConfigurationObject : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingProviderForConfigurationObject"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MissingProviderForConfigurationObject(Type type) : base($"There are no providers for '{type.GetFriendlyConfigurationName()}' - '{type.Name}'")
        {

        }
    }
}
