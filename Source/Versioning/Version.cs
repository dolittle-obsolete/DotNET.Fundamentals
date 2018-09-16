/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Versioning
{
    /// <summary>
    /// Represents a version number adhering to the SemVer 2.0 standard
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Version"/>
        /// </summary>
        /// <param name="major">Major version of the software</param>
        /// <param name="minor">Minor version of the software</param>
        /// <param name="patch">Path level of the software</param>
        /// <param name="build">Build number of the software</param>
        /// <param name="isPrerelease">Wether or not the software is pre-release - default false</param>
        /// <param name="preReleaseString">If prerelease - the prerelease string</param>
        public Version(
            int major,
            int minor,
            int patch,
            int build,
            bool isPrerelease = false,
            string preReleaseString = "")
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = build;
            IsPrerelease = isPrerelease;
            PreReleaseString = preReleaseString;

        }

        /// <summary>
        /// Gets the major version number of the software
        /// </summary>
        public int Major {  get; }

        /// <summary>
        /// Gets the minor version number of the software
        /// </summary>
        public int Minor { get; }

        /// <summary>
        /// Gets the patch level of the software
        /// </summary>
        public int Patch {  get; }

        /// <summary>
        /// Gets the build number of the software
        /// </summary>
        public int Build { get; }

        /// <summary>
        /// Gets the prerelease string
        /// </summary>
        public string PreReleaseString {  get; }

        /// <summary>
        /// Gets whether or not the software is a prerelease
        /// </summary>
        public bool IsPrerelease { get; }
    }
}