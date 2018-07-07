/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;

namespace Dolittle.Applications
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
            if (Segments.Count() != other.Segments.Count()) return false;
            // Use Equals on each segment instead?
            return GetHashCode().Equals(other.GetHashCode());
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is IApplicationLocation other) 
            {
                return Equals(other);
            }
            return false;
        }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationLocation x, ApplicationLocation y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationLocation x, ApplicationLocation y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = 0;
            Segments.ForEach(segment => hashCode += segment.GetHashCode());
            return hashCode;
        }
    }
}