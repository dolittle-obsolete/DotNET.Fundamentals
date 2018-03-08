/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a location in the <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationLocation : IEquatable<IApplicationLocation>, IComparable, IComparable<IApplicationLocation>
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationSegment">segments</see> for the <see cref="IApplicationLocation"/>
        /// </summary>
        IEnumerable<IApplicationLocationSegment>    Segments { get; }
    }
}