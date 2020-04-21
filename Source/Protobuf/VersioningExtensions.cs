// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using Dolittle.Versioning;
using grpc = contracts::Dolittle.Versioning.Contracts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common execution types.
    /// </summary>
    public static class VersioningExtensions
    {
        /// <summary>
        /// Convert a <see cref="Version"/> to <see cref="grpc.Version"/>.
        /// </summary>
        /// <param name="version"><see cref="Version"/> to convert from.</param>
        /// <returns>Converted <see cref="grpc.Version"/>.</returns>
        public static grpc.Version ToProtobuf(this Version version) =>
            new grpc.Version
                {
                    Major = version.Major,
                    Minor = version.Minor,
                    Patch = version.Patch,
                    Build = version.Build,
                    PreReleaseString = version.PreReleaseString
                };

        /// <summary>
        /// Convert a <see cref="grpc.Version"/> to <see cref="Version"/>.
        /// </summary>
        /// <param name="version"><see cref="grpc.Version"/> to convert from.</param>
        /// <returns>Converted <see cref="Version"/>.</returns>
        public static Version ToVersion(this grpc.Version version) =>
            new Version(version.Major, version.Minor, version.Patch, version.Build, version.PreReleaseString);
    }
}