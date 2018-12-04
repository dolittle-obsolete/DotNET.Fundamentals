/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Configuration.Files
{
    /// <summary>
    /// Exception that gets thrown when there are multiple <see cref="ICanParseConfigurationFile">parsers</see> for 
    /// the same configuration file
    /// </summary>
    public class MultipleParsersForConfigurationFile : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleParsersForConfigurationFile"/>
        /// </summary>
        /// <param name="filename">Name of the file</param>
        public MultipleParsersForConfigurationFile(string filename) : base($"Multiple parsers for '{filename}' - unable to decide which to use") {}
    }
}