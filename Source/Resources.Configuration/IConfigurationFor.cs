/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// Represents a configuration for a Resource
    /// </summary>
    /// <typeparam name="T">The type of the Configuration</typeparam>
    public interface IConfigurationFor<T> where T : class
    {
        /// <summary>
        /// Retrieves the configuration instance
        /// </summary>
        T Instance {get; } 
    }
}