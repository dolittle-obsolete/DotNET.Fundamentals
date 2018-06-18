/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents the generation of an <see cref="IArtifact"/>
    /// </summary>
    public class ArtifactGeneration : ConceptAs<int>, IArtifactGeneration
    {
        /// <inheritdoc/>
        public string AsString()
        {
            return $"Gen.{Value}";
        }

        /// <inheritdoc/>
        public int GenerationNumber()
        {
            return Value;
        }

        /// <summary>
        /// Implicitely converts <see cref="int"/> to an <see cref="ArtifactGeneration"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ArtifactGeneration(int value) 
        {
            return new ArtifactGeneration { Value = value };
        }
    }
}