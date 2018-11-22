/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// THe exception that gets thrown when an invalid <see cref="IAmAResourceType"/> is discovered
    /// </summary>
    public class InvalidResourceTypeFound : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InvalidResourceTypeFound"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public InvalidResourceTypeFound(string message) : base(message)
        {
        }
    }
}