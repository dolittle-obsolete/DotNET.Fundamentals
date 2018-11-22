/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when multiple implementations for the same service is discovered
    /// </summary>
    public class MultipleImplementationsFoundForService : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="MultipleImplementationsFoundForService"/>
        /// </summary>
        /// <param name="service"></param>
        public MultipleImplementationsFoundForService(Type service)
            : base($"Multiple implementations for the service {service.FullName} was found")
        { }
    }
}