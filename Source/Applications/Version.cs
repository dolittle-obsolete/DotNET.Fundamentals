/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Text.RegularExpressions;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the 
    /// </summary>
    public class Version : Value<Version>
    {
        static Regex versionRegex = new Regex(@"(\d+).(\d+).(\d+)-*([\w]+)*[+-.]*(\d+)*", RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new version number
        /// </summary>
        /// <param name="major">Major version number</param>
        /// <param name="minor">Minor version number</param>
        /// <param name="patch">Patch version number</param>
        /// <param name="build">Build number</param>
        /// <param name="preReleaseTag">Optional pre-release tag</param>
        /// <param name="commit">Optional commit association</param>
        public Version(int major, int minor, int patch, int build, string preReleaseTag = "", string commit = "")
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = build;
            PreReleaseTag = preReleaseTag;
            Commit = commit;
        }

        /// <summary>
        /// Gets the major part of the version of the <see cref="BoundedContext"/>
        /// </summary>
        public int Major {  get; }

        /// <summary>
        /// Gets the minor part of the version of the <see cref="BoundedContext"/>
        /// </summary>
        public int Minor {  get; }

        /// <summary>
        /// Gets the patch part of the version of the <see cref="BoundedContext"/>
        /// </summary>
        public int Patch {  get; }

        /// <summary>
        /// Gets the build part of the version of the <see cref="BoundedContext"/>
        /// </summary>
        public int Build { get; }

        /// <summary>
        /// Gets the pre-release tag part of the <see cref="BoundedContext"/>
        /// </summary>
        public string PreReleaseTag {  get; }

        /// <summary>
        /// Gets whether or not the version is a pre-release or not
        /// </summary>
        public bool IsPreRelease => !string.IsNullOrEmpty(PreReleaseTag);

        /// <summary>
        /// Associated commit SHA for the <see cref="BoundedContext"/>
        /// </summary>
        public string Commit {  get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (IsPreRelease)
                return $"{Major}.{Minor}.{Patch}-{PreReleaseTag}.{Build}";
            else
                return $"{Major}.{Minor}.{Patch}.{Build}";
        }

        /// <inheritdoc/>
        public static Version FromString(string versionAsString)
        {
            var versionResult = versionRegex.Match(versionAsString);
            ThrowIfInvalidString(versionAsString, versionResult);
            var major = 0;
            var minor = 0;
            var patch = 0;
            var build = 0;
            try
            {
                major = int.Parse(versionResult.Groups[1].Value);
                minor = int.Parse(versionResult.Groups[2].Value);
                patch = int.Parse(versionResult.Groups[3].Value);
                build = versionResult.Groups.Count == 6 ?
                int.Parse(versionResult.Groups[5].Value) :
                int.Parse(versionResult.Groups[4].Value);
            }
            catch (FormatException ex)
            {
                throw new InvalidVersionString(versionAsString, ex);
            }

            if (versionResult.Groups.Count == 6) return new Version(major, minor, patch, build, versionResult.Groups[4].Value);
            else return new Version(major, minor, patch, build);
        }

        static void ThrowIfInvalidString(string versionAsString, Match versionResult)
        {
            if (!versionResult.Success) throw new InvalidVersionString(versionAsString);
        }
    }
}