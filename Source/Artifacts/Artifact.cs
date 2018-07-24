/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the concept of an artifact
    /// </summary>
    public class Artifact : Value<Artifact>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Artifact"/>
        /// </summary>
        /// <param name="id"><see cref="Guid">Id</see> of the <see cref="Artifact"/></param>
        /// <param name="generation"><see cref="ArtifactGeneration">Generation</see> of the <see cref="Artifact"/></param>
        public Artifact(Guid id, ArtifactGeneration generation)
        {
            Id = id;
            Generation = generation;
        }

        /// <inheritdoc/>
        public Guid Id { get; }
        
        /// <inheritdoc/>
        public ArtifactGeneration Generation {get; }
    }
}