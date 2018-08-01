/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the concept of a unique identifier for an artifact
    /// </summary>
    public class ArtifactId : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to an <see cref="ArtifactId"/>
        /// </summary>
        public static implicit operator ArtifactId(Guid id)
        {
            return new ArtifactId { Value = id };
        }

        /// <summary>
        /// Create a new <see cref="ArtifactId"/>
        /// </summary>
        /// <returns><see cref="ArtifactId">New artifact identifier</see></returns>
        public static ArtifactId New()
        {
            return Guid.NewGuid();
        }
    }
}