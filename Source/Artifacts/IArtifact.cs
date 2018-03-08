/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Artifacts
{
    /// <summary>
    /// Defines an artifact in the system
    /// </summary>
    public interface IArtifact
    {
        /// <summary>
        /// Gets the name of the <see cref="IArtifact"/>
        /// </summary>
        ArtifactName  Name { get; }

        /// <summary>
        /// Gets the type of the <see cref="IArtifact"/>
        /// </summary>
        IArtifactType Type { get; }
    }
}