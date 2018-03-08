/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents the configuration for a specific service for a resource
    /// </summary>
    public class ResourceService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceService"/>
        /// </summary>
        /// <param name="service">Service for the resource</param>
        public ResourceService(Type service)
        {
            Service = service;
        }

        /// <summary>
        /// Gets the service the configuration is for
        /// </summary>
        public Type Service { get; }
    }
}