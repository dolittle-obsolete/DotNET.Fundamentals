/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Text.RegularExpressions;

namespace Dolittle.Versioning
{
    /// <summary>
    /// Represents an implementation of <see cref="IVersionConverter"/>
    /// </summary>
    public class VersionConverter : IVersionConverter
    {
        static Regex _versionRegex = new Regex("(\\d+).(\\d+).(\\d+)-*([\\w]+)*[+-.]*(\\d+)*", RegexOptions.Compiled);

        /// <inheritdoc/>
        public Version FromString(string versionAsString)
        {
            var result = _versionRegex.Match(versionAsString);
            if( !result.Success ) throw new InvalidVersionString(versionAsString);
            var major = int.Parse(result.Groups[1].Value);
            var minor = int.Parse(result.Groups[2].Value);
            var patch = int.Parse(result.Groups[3].Value);
            var buildGroup = result.Groups[5].Value == string.Empty ? 4 : 5;
            var build = 0;
            int.TryParse(result.Groups[buildGroup].Value, out build);
            var isRelease = result.Groups[4].Value == string.Empty;

            if( !isRelease )
                return new Version(major, minor, patch, build, false, result.Groups[4].Value);
            else
                return new Version(major, minor, patch, build, true);
        }

        /// <inheritdoc/>
        public string ToString(Version version)
        {
            throw new System.NotImplementedException();
        }
    }
}