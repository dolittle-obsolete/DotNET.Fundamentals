/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Artifacts
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
        /// <param name="generation"><see cref="ArtifactGeneration">Generation</see> of the <see cref="Artifact"/></param>
        public Artifact(ArtifactName name, IArtifactType type, ArtifactGeneration generation)
        {
            Name = name;
            Type = type;
            Generation = generation;
        }

        /// <inheritdoc/>
        public ArtifactName Name { get; }
        
        /// <inheritdoc/>
        public IArtifactType Type { get; }

        /// <inheritdoc/>
        public ArtifactGeneration Generation {get; }

        /// <inheritdoc/>
        public static bool operator ==(Artifact x, Artifact y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(Artifact x, Artifact y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public bool Equals(Artifact other)
        {
            if (Name.Value != other.Name.Value) return false;

            if (Type.Identifier != other.Type.Identifier) return false;
            if (Generation.Value != other.Generation.Value ) return false;

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = Name.Value.GetHashCode();
            hashCode += Type.Identifier.GetHashCode();
            hashCode += Generation.Value.GetHashCode();

            return hashCode;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Artifact)) return false;
            return Equals((Artifact) obj);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(Artifact other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }
    }
}