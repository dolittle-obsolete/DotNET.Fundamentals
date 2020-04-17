// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.Artifacts;
using grpc = contracts::Dolittle.Artifacts.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common execution types.
    /// </summary>
    public static class ArtifactsExtensions
    {
        /// <summary>
        /// Convert a <see cref="Artifact"/> to <see cref="grpc.Artifact"/>.
        /// </summary>
        /// <param name="artifact"><see cref="Artifact"/> to convert from.</param>
        /// <returns>Converted <see cref="grpc.Artifact"/>.</returns>
        public static grpc.Artifact ToProtobuf(this Artifact artifact) =>
            new grpc.Artifact { Id = artifact.Id.ToProtobuf(), Generation = artifact.Generation.Value };

        /// <summary>
        /// Convert a <see cref="grpc.Artifact"/> to <see cref="Artifact"/>.
        /// </summary>
        /// <param name="artifact"><see cref="grpc.Artifact"/> to convert from.</param>
        /// <returns>Converted <see cref="Artifact"/>.</returns>
        public static Artifact ToArtifact(this grpc.Artifact artifact) =>
            new Artifact(artifact.Id.To<ArtifactId>(), artifact.Generation);
    }
}