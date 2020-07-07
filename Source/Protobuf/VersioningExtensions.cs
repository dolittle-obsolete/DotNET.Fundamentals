// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Versioning;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common execution types.
    /// </summary>
    public static class VersioningExtensions
    {
        /// <summary>
        /// Convert a <see cref="Version"/> to <see cref="Versioning.Contracts.Version"/>.
        /// </summary>
        /// <param name="version"><see cref="Version"/> to convert from.</param>
        /// <returns>Converted <see cref="Versioning.Contracts.Version"/>.</returns>
        public static Versioning.Contracts.Version ToProtobuf(this Version version) =>
            new Versioning.Contracts.Version
                {
                    Major = version.Major,
                    Minor = version.Minor,
                    Patch = version.Patch,
                    Build = version.Build,
                    PreReleaseString = version.PreReleaseString
                };

        /// <summary>
        /// Convert a <see cref="Versioning.Contracts.Version"/> to <see cref="Version"/>.
        /// </summary>
        /// <param name="version"><see cref="Versioning.Contracts.Version"/> to convert from.</param>
        /// <returns>Converted <see cref="Version"/>.</returns>
        public static Version ToVersion(this Versioning.Contracts.Version version) =>
            new Version(version.Major, version.Minor, version.Patch, version.Build, version.PreReleaseString);
    }
}