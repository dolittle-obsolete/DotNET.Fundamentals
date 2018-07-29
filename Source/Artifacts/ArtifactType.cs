/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the concept of a type of <see cref="Artifact"/>
    /// </summary>
    public class ArtifactType : ConceptAs<Guid>
    {
         /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to an <see cref="ArtifactType"/>
        /// </summary>
        public static implicit operator ArtifactType(Guid id)
        {
            return new ArtifactType { Value = id };
        }
       

    }
}