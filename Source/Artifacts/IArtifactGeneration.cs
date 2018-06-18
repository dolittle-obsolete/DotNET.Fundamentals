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
        /// Returns an <see cref="string"/> representation
        /// </summary>
        /// <returns><see cref="string"/> representation of the <see cref="IArtifactGeneration"/></returns>
        string AsString();

        /// <summary>
        /// Returns the generation number
        /// </summary>
        int GenerationNumber();
    }
}