/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Defines a system that can provide <see cref="IArtifactType"/>
    /// </summary>
    public interface ICanProvideArtifactTypes
    {
        /// <summary>
        /// Provide <see cref="IArtifactType">artifact types</see>
        /// </summary>
        /// <returns><see cref="IEnumerable{IArtifactType}">Artifact types</see></returns>
         IEnumerable<IArtifactType> Provide();
    }
}