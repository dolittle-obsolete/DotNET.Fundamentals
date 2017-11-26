/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Applications
{
    /// <summary>
    /// Defines a system that is capable of converting between <see cref="IApplicationArtifactIdentifier"/>
    /// and other representations, typically a <see cref="string"/>
    /// </summary>
    public interface IApplicationArtifactIdentifierStringConverter
    {
        /// <summary>
        /// Get a string representation of the resource
        /// </summary>
        /// <param name="identifier"><see cref="IApplicationArtifactIdentifier">Resource</see> to represent as string</param>
        /// <returns><see cref="string"/> representing the resource</returns>
        string AsString(IApplicationArtifactIdentifier identifier);

        /// <summary>
        /// Translate a <see cref="string"/> to a <see cref="IApplicationArtifactIdentifier"/>
        /// </summary>
        /// <param name="identifierAsString"><see cref="string"/> representing the resource</param>
        /// <returns><see cref="IApplicationArtifactIdentifier">Identifier</see> for the resource</returns>
        IApplicationArtifactIdentifier FromString(string identifierAsString);
    }
}
