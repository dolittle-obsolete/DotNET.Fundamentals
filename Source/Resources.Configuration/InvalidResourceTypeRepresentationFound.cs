/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// THe exception that gets thrown when an invalid <see cref="IRepresentAResourceType"/> is discovered
    /// </summary>
    public class InvalidResourceTypeRepresentationFound : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="InvalidResourceTypeRepresentationFound"/>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public InvalidResourceTypeRepresentationFound(string message) : base(message)
        { }
    }
}