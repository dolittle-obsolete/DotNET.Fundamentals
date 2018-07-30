/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a location within an application
    /// </summary>
    public interface IApplicationLocationSegment : IEquatable<IApplicationLocationSegment>, IComparable, IComparable<IApplicationLocationSegment>
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationSegmentName">application location name</see>
        /// </summary>
        IApplicationLocationSegmentName Name { get; }
        
        /// <summary>
        /// Gets the children <see cref="IEnumerable{IApplicationLocationSegment}">location fragments</see>
        /// </summary>
        IEnumerable<IApplicationLocationSegment> Children {get; }

        /// <summary>
        /// Add a <see cref="IApplicationLocationSegment">child</see>
        /// </summary>
        /// <param name="child"><see cref="IApplicationLocationSegment">child</see> to add</param>
        void AddChild(IApplicationLocationSegment child);

    }

    /// <summary>
    /// Defines a location within the application
    /// </summary>
    public interface IApplicationLocationSegment<TName> : IApplicationLocationSegment
        where TName: IApplicationLocationSegmentName
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationSegmentName">name</see> of the location
        /// </summary>
        new TName Name { get; }
    }
}
