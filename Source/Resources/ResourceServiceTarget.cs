/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents a target for a <see cref="ResourceService"/>
    /// </summary>   
    public class ResourceServiceTarget
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceServiceTarget"/>
        /// </summary>
        /// <param name="service">Service it represents an implementation of</param>
        /// <param name="name">Name of the target</param>
        /// <param name="target">Type to use for target</param>
        public ResourceServiceTarget(ResourceService service, string name, Type target)
        {
            ThrowIfTargetIsUnassignableToService(service, target);
            Service = service;
            Name = name;
            Target = target;
        }

        /// <summary>
        /// Gets the <see cref="ResourceService"/> it targets
        /// </summary>
        public ResourceService Service { get; }

        /// <summary>
        /// Gets the name associated with the target
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the target type for the <see cref="ResourceService"/>
        /// </summary>
        public Type Target { get; }

        void ThrowIfTargetIsUnassignableToService(ResourceService service, Type target)
        {
            if( !service.Service.IsAssignableFrom(target) ) 
                throw new TargetServiceIsUnassignableToSourceService(service.Service, target);
        }
    }
}