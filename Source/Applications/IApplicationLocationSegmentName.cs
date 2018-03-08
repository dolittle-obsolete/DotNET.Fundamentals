/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Defines the name of an <see cref="IApplicationLocationSegment"/>
    /// </summary>
    public interface IApplicationLocationSegmentName
    {
        /// <summary>
        /// Returns a <see cref="string"/> representation
        /// </summary>
        /// <returns><see cref="string"/> representation of the <see cref="IApplicationLocationSegmentName"/></returns>
        string AsString();
    }
}