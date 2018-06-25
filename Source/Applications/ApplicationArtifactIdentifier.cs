/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolittle.Artifacts;
using Dolittle.Collections;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationArtifactIdentifier"/> - an identifier for <see cref="IArtifact">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public class ApplicationArtifactIdentifier : IApplicationArtifactIdentifier
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationArtifactIdentifier"/>
        /// </summary>
        /// <param name="application"><see cref="IApplication"/> the resource belongs to</param>
        /// <param name="area"><see cref="ApplicationArea"/> the resource belongs to</param>
        /// <param name="location"><see cref="IApplicationLocation">Location</see> for the <see cref="IArtifact"/></param>
        /// <param name="artifact"><see cref="IArtifact">Artifact</see> the identifier is for</param>
        public ApplicationArtifactIdentifier(
            IApplication application,
            ApplicationArea area,
            IApplicationLocation location,
            IArtifact artifact)
        {
            Application = application;
            Area = area;
            Location = location;
            Artifact = artifact;
        }

        /// <inheritdoc/>
        public IApplication Application { get; }

        /// <inheritdoc/>
        public ApplicationArea Area { get; }

        /// <inheritdoc/>
        public IApplicationLocation Location { get; }

        /// <inheritdoc/>
        public IArtifact Artifact { get; }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationArtifactIdentifier x, ApplicationArtifactIdentifier y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationArtifactIdentifier x, ApplicationArtifactIdentifier y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationArtifactIdentifier other)
        {
            if (!Location.Equals(other.Location)) return false;

            if (Application.Name.Value != other.Application.Name.Value) return false;
            if (Area.Value != other.Area.Value ) return false;

            if (Artifact.Name.Value != other.Artifact.Name.Value) return false;
            if (Artifact.Type.Identifier != other.Artifact.Type.Identifier) return false;
            if (Artifact.Generation.Value != other.Artifact.Generation.Value) return false;

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = Application.Name.GetHashCode();
            // TODO: Shouldnt hashCode += Artifact.GetHashCode() instead?
            // we can have multiple different artifacts with the same ArtifactName, but have a different
            // ArtifactGeneration for example, do we want to give those the same HashCode?
            hashCode += Artifact.Name.GetHashCode();
            hashCode += Area.Value.GetHashCode();
            hashCode += Location.GetHashCode();

            return hashCode;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is IApplicationArtifactIdentifier)) return false;
            return Equals((IApplicationArtifactIdentifier) obj);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationArtifactIdentifier other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public override string ToString() 
        {
            var stringBuilder = new StringBuilder();
            //TODO: Isn't there more parts of the ApplicationArtifactIdentifier that needs to be a part of this string?
            stringBuilder.Append(Application.Name.ToString());
            stringBuilder.Append($" - {Area.Value}");
            stringBuilder.Append($" - {Artifact.Name.ToString()}");
            stringBuilder.Append($" @ {Location.ToString()}");
           
            return stringBuilder.ToString();
        }
    }
}