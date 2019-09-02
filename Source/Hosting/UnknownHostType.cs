/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Hosting
{
    /// <summary>
    /// The exception that gets thrown if a <see cref="HostType"/> is unknown
    /// </summary>
    public class UnknownHostType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownHostType"/>
        /// </summary>
        /// <param name="type">Unknown <see cref="HostType"/></param>
        public UnknownHostType(HostType type) : base($"Unknown host type '{type}'") {}
    }
}