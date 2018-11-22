/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The Exception that gets thrown when the resources file is not present
    /// </summary>
    public class MissingResourcesFile : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="MissingResourcesFile"/>
        /// </summary>
        public MissingResourcesFile()
            : base($"Could not find .dolittle/resources.json")
        {
        }
    }
}