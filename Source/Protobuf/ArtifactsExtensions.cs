// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Artifacts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common execution types.
    /// </summary>
    public static class ArtifactsExtensions
    {
        /// <summary>
        /// Convert a <see cref="Artifact"/> to <see cref="Artifacts.Contracts.Artifact"/>.
        /// </summary>
        /// <param name="artifact"><see cref="Artifact"/> to convert from.</param>
        /// <returns>Converted <see cref="Artifacts.Contracts.Artifact"/>.</returns>
        public static Artifacts.Contracts.Artifact ToProtobuf(this Artifact artifact) =>
            new Artifacts.Contracts.Artifact { Id = artifact.Id.ToProtobuf(), Generation = artifact.Generation.Value };

        /// <summary>
        /// Convert a <see cref="Artifacts.Contracts.Artifact"/> to <see cref="Artifact"/>.
        /// </summary>
        /// <param name="artifact"><see cref="Artifacts.Contracts.Artifact"/> to convert from.</param>
        /// <returns>Converted <see cref="Artifact"/>.</returns>
        public static Artifact ToArtifact(this Artifacts.Contracts.Artifact artifact) =>
            new Artifact(artifact.Id.To<ArtifactId>(), artifact.Generation);
    }
}