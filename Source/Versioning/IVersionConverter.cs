/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Versioning
{
    /// <summary>
    /// Defines a system that is capable of converting <see cref="Version"/> to and from <see cref="string"/>
    /// </summary>
    public interface IVersionConverter
    {
        /// <summary>
        /// Convert from <see cref="string"/> to <see cref="Version"/>
        /// </summary>
        /// <param name="versionAsString">Version as <see cref="string"/></param>
        /// <returns>Converter <see cref="Version"/></returns>
        Version FromString(string versionAsString);

        /// <summary>
        /// Convert from <see cref="Version"/> to <see cref="string"/>
        /// </summary>
        /// <param name="version"><see cref="Version"/> to convert from</param>
        /// <returns><see cref="string"/> version</returns>
        string ToString(Version version);
    }
}