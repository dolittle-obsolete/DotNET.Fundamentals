/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the name of a <see cref="IArtifact"/> 
    /// </summary>
    public class ArtifactName : ConceptAs<string>, IArtifactName
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return this;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="ArtifactName"/>
        /// </summary>
        /// <param name="artifactName">Name of the <see cref="ArtifactName"/></param>
        public static implicit operator ArtifactName(string artifactName)
        {
            return new ArtifactName { Value = artifactName };
        }
    }
}
