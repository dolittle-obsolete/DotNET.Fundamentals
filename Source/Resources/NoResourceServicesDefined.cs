/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Resources
{
    /// <summary>
    /// Exception that gets thrown when there are no <see cref="ResourceService"/> defined for a resource
    /// </summary>
    public class NoResourceServicesDefined : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NoResourceServicesDefined"/>
        /// </summary>
        /// <param name="resource">Name of the resource missing <see cref="ResourceService">resource services</see></param>
        public NoResourceServicesDefined(string resource)
            : base($"No resource services defined for resource '{resource}'")
        { }
    }
}