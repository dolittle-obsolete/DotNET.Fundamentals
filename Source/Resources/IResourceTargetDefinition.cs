/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Resources
{
    /// <summary>
    /// Defines a definition for a target for a resource
    /// </summary>
    public interface IResourceTargetDefinition
    {
        /// <summary>
        /// Gets the <see cref="IResourceDefinition"/> source
        /// </summary>
        IResourceDefinition Source { get; }

        /// <summary>
        /// Gets the <see cref="ResourceServiceTarget">resource service targets</see> exposed
        /// </summary>
        IEnumerable<ResourceServiceTarget> Targets { get;}

        /// <summary>
        /// Checks if there is a <see cref="ResourceServiceTarget"/> registered in the definition
        /// </summary>
        /// <param name="name">Name of the <see cref="ResourceServiceTarget"/></param>
        /// <returns>True if exists, false if not</returns>
        bool HasServiceTarget(string name);

        /// <summary>
        /// Get a <see cref="ResourceServiceTarget"/> by name
        /// </summary>
        /// <param name="name">Name of the <see cref="ResourceServiceTarget"/></param>
        /// <returns>The instance of the <see cref="ResourceServiceTarget"/></returns>
        ResourceServiceTarget GetServiceTarget(string name);
    }
}