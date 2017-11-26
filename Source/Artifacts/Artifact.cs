/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Artifacts
{
    /// <summary>
    /// Represents an implementation of <see cref="IArtifact"/>
    /// </summary>
    public class Artifact : IArtifact
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Artifact"/>
        /// </summary>
        /// <param name="name"><see cref="ArtifactName">Name</see> of the <see cref="Artifact"/></param>
        /// <param name="type"><see cref="IArtifactType">Type</see> of the <see cref="Artifact"/></param>
        public Artifact(ArtifactName name, IArtifactType type)
        {
            Name = name;
            Type = type;
        }

        /// <inheritdoc/>
        public ArtifactName Name { get; }
        
        /// <inheritdoc/>
        public IArtifactType Type { get; }
    }
}