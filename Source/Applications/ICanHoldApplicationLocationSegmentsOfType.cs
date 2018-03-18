/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines something that can hold <see cref="IApplicationLocationSegment">application location fragments</see>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IApplicationLocationSegment"/></typeparam>
    public interface ICanHoldApplicationLocationSegmentsOfType<T>
        where T:IApplicationLocationSegment
    {
        /// <summary>
        /// Gets the children <see cref="IEnumerable{IApplicationLocationSegment}">location fragments</see>
        /// </summary>
        IEnumerable<IApplicationLocationSegment> Children { get; }
    }
}
