/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an identifier for a service type
    /// </summary>
    public enum EndpointType
    {
        /// <summary>
        /// Represents public endpoints
        /// </summary>
        Public=1,

        /// <summary>
        /// Represents private endpoints
        /// </summary>
        Private
    }
}