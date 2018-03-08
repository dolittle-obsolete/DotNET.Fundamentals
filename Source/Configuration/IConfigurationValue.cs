/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Configuration
{
    /// <summary>
    /// Defines a configurable value
    /// </summary>
    public interface IConfigurationValue
    {
        /// <summary>
        /// Gets the value
        /// </summary>
        object Value { get; }
    }
}