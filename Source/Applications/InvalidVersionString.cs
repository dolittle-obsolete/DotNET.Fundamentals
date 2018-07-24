/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception that gets thrown when a 
    /// </summary>
    public class InvalidVersionString : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidVersionString"/>
        /// </summary>
        /// <param name="versionAsString">The invalid version string</param>
        public InvalidVersionString(string versionAsString) : this(versionAsString, null) {}

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidVersionString"/>
        /// </summary>
        /// <param name="versionAsString">The invalid version string</param>
        /// <param name="innerException"><see cref="Exception">Inner exception</see></param>
        public InvalidVersionString(string versionAsString, Exception innerException) : base($"Version '{versionAsString}' is not a valid version string",innerException) {}
    }
}
