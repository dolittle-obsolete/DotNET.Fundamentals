/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace doLittle.Applications
{
    /// <summary>
    /// Represents a null implementation for <see cref="IApplicationStructureFragment"/>
    /// </summary>
    public class NullApplicationStructureFragment : IApplicationStructureFragment
    {
        /// <summary>
        /// Gets an instance of the <see cref="NullApplicationStructureFragment"/>
        /// </summary>
        /// <returns></returns>
        public static readonly NullApplicationStructureFragment Instance = new NullApplicationStructureFragment();

        NullApplicationStructureFragment()
        {
            Type = typeof(IApplicationLocationSegment);
            Parent = this;
            Required = false;
            Recursive = false;
            Children = new IApplicationStructureFragment[0];
        }

        /// <inheritdoc/>
        public Type Type { get; }

        /// <inheritdoc/>
        public bool Required { get; }

        /// <inheritdoc/>
        public IApplicationStructureFragment Parent { get; }

        /// <inheritdoc/>
        public bool HasParent => false;

        /// <inheritdoc/>
        public IEnumerable<IApplicationStructureFragment> Children { get; }

        /// <inheritdoc/>
        public bool Recursive { get; }
    }
}