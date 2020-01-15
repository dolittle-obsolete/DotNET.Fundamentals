// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the generation of an <see cref="Artifact"/>.
    /// </summary>
    public class ArtifactGeneration : ConceptAs<int>
    {
        /// <summary>
        /// Gets the first generation representation.
        /// </summary>
        public static readonly ArtifactGeneration First = new ArtifactGeneration { Value = 1 };

        /// <summary>
        /// Implicitly converts <see cref="int"/> to an <see cref="ArtifactGeneration"/>.
        /// </summary>
        /// <param name="value">Converted <see cref="ArtifactGeneration"/>.</param>
        public static implicit operator ArtifactGeneration(int value) => new ArtifactGeneration { Value = value };
    }
}