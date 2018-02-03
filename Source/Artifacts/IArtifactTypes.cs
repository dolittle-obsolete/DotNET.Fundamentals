/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Artifacts
{
    /// <summary>
    /// Defines a system that represents <see cref="IArtifactType">artifact types</see>
    /// </summary>
    public interface IArtifactTypes
    {
        /// <summary>
        /// Gets all the <see cref="IArtifactType">artifact types</see>
        /// </summary>
        IEnumerable<IArtifactType>  All { get; }

        /// <summary>
        /// Check if <see cref="IArtifactType"/> by exists by its identifier
        /// </summary>
        /// <param name="identifier">Name of the <see cref="IArtifactType"/></param>
        /// <returns>True if it has it, false if not</returns>
        bool Exists(string identifier);

        /// <summary>
        /// Get an <see cref="IArtifactType"/> by the identifer of the type
        /// </summary>
        /// <param name="identifier">Name of the <see cref="IArtifactType"/></param>
        /// <returns><see cref="IArtifactType"/> found</returns>
        IArtifactType GetFor(string identifier);
    }
}