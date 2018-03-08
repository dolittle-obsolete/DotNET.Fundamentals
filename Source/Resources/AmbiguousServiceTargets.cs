/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resources
{
    /// <summary>
    /// Exception that gets thrown when there are ambiguous service targets - same name for multiple for same service
    /// </summary>
    public class AmbiguousServiceTargets : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AmbiguousServiceTargets"/>
        /// </summary>
        /// <param name="resource"><see cref="IResourceDefinition">The Resource</see></param>
        /// <param name="name">Name of the service target</param>
        public AmbiguousServiceTargets(IResourceDefinition resource, string name) : base($"Resource '{resource.Name}' has multiple service targets with name '{name}'")
        {

        }
    }
}