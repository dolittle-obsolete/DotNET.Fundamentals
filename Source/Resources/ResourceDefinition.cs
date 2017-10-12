/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Resources
{
    /// <summary>
    /// Defines a resource and its requirements for configuration
    /// </summary>
    public class ResourceDefinition : IResourceDefinition
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceDefinition"/>
        /// </summary>
        /// <param name="name">Name of the resource</param>
        /// <param name="services">Services associated with the resource</param>
        /// <remarks>
        /// The name of the resource is unique in the system
        /// </remarks>
        public ResourceDefinition(
            string name,
            IEnumerable<ResourceService> services)
        {
            Name = name;
            Services = services;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IEnumerable<ResourceService>   Services { get;}
    }
}