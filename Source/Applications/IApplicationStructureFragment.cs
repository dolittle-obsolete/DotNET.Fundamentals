/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace doLittle.Applications
{
    /// <summary>
    /// Defines a location segment within an <see cref="IApplicationStructure"/>
    /// </summary>
    public interface IApplicationStructureFragment
    {
        /// <summary>
        /// Gets the type of segment - must be a type of <see cref="IApplicationLocationFragment"/>
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets whether or not the <see cref="IApplicationLocationFragment"/> is required
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Gets whether or not the <see cref="IApplicationLocationFragment"/> can be recursive in the structure
        /// </summary>
        bool Recursive { get; }

        /// <summary>
        /// Gets the parent <see cref="IApplicationStructureFragment"/>
        /// </summary>
        IApplicationStructureFragment Parent { get; }

        /// <summary>
        /// Gets whether or not this <see cref="IApplicationStructureFragment">segment</see> has a parent
        /// </summary>
        bool HasParent { get; }

        /// <summary>
        /// Gets child <see cref="IApplicationStructureFragment">fragments</see>
        /// </summary>
        /// <returns><see cref="IEnumerable{IApplicationStructureFragment}">Fragments</see></returns>
        IEnumerable<IApplicationStructureFragment> Children { get; }
    }
}
