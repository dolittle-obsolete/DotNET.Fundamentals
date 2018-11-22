/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when no implementation is found for a service
    /// </summary>
    public class ImplementationForServiceNotFound : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="ImplementationForServiceNotFound"/>
        /// </summary>
        /// <param name="service"></param>
        public ImplementationForServiceNotFound(Type service)
            : base($"No implementation was found for the service {service.FullName}")
        {
        }
    }
}