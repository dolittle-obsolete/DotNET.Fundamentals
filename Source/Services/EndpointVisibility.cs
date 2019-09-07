/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Services
{
    /// <summary>
    /// Represents the visibility for an <see cref="IEndpoint"/>
    /// </summary>
    public enum EndpointVisibility
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