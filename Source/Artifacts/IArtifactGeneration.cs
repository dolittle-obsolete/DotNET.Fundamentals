/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the generation of an <see cref="IArtifact"/>
    /// </summary>
    public interface IArtifactGeneration
    {
        /// <summary>
        /// Returns the generation number
        /// </summary>
        int GenerationNumber();
    }
}