/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Artifacts
{
    /// <summary>
    /// Represents the name of an <see cref="IArtifact"/>
    /// </summary>
    public interface IArtifactName
    {
        /// <summary>
        /// Returns a <see cref="string"/> representation
        /// </summary>
        /// <returns><see cref="string"/> representation of the <see cref="IArtifactName"/></returns>
        string AsString();
    }
}
