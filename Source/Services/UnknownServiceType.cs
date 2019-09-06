/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Services
{
    /// <summary>
    /// The exception that gets thrown if a <see cref="ServiceType"/> is unknown
    /// </summary>
    public class UnknownServiceType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownServiceType"/>
        /// </summary>
        /// <param name="type">Unknown <see cref="ServiceType"/></param>
        public UnknownServiceType(ServiceType type) : base($"Unknown service type '{type}'") {}
    }
}