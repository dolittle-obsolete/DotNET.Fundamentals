/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Collections;

namespace doLittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationLocation"/>
    /// </summary>
    public class ApplicationLocation : IApplicationLocation
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationLocation"/>
        /// </summary>
        /// <param name="segments"><see cref="IApplicationLocationSegment">Segments</see> for the location</param>
        public ApplicationLocation(IEnumerable<IApplicationLocationSegment> segments)
        {
            Segments = segments;
        }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Segments { get; }

        /// <inheritdoc/>
        public int CompareTo(object other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationLocation other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationLocation other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        /// <inheritdoc/>
        public override bool Equals(object other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationLocation x, ApplicationLocation y)
        {
            return x.GetHashCode().Equals(y.GetHashCode());
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationLocation x, ApplicationLocation y)
        {
            return !x.GetHashCode().Equals(y.GetHashCode());
        }
        

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 0;
            Segments.ForEach(segment => hashCode += segment.Name.AsString().GetHashCode());
            return hashCode;
        }
    }
}