/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

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
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public int CompareTo(IApplicationLocation other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationLocation other)
        {
            throw new NotImplementedException();
        }
    }
}