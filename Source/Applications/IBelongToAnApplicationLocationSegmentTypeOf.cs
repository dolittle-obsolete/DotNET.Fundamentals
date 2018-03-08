/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a parent relationship to another <see cref="IApplicationLocationSegment"/> of a specific
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IApplicationLocationSegment"/></typeparam>
    public interface IBelongToAnApplicationLocationSegmentTypeOf<T>
        where T : IApplicationLocationSegment
    {
        /// <summary>
        /// Gets the parent <see cref="IApplicationLocationSegment">location</see>
        /// </summary>
        T Parent { get; }
    }
}