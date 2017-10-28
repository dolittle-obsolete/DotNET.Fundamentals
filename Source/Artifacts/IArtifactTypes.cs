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
    }
}