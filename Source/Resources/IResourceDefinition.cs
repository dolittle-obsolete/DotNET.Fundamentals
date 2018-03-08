/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// Defines a definition for a resource
    /// </summary>
    public interface IResourceDefinition
    {
        /// <summary>
        /// Get the name of the resource the definition is for
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="ResourceService">services</see> for the resource
        /// </summary>
        IEnumerable<ResourceService>    Services { get; }
    }
}