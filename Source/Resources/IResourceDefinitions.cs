/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Resources
{

    /// <summary>
    /// Defines a system that knows about <see cref="ResourceDefinition">resource definitions</see>
    /// </summary>
    public interface IResourceDefinitions
    {
        /// <summary>
        /// Gets all the resource definitions
        /// </summary>
        IEnumerable<IResourceDefinition> All { get; }
    }
}