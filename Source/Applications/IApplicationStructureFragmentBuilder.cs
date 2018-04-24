/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a builder for building <see cref="IApplicationStructureFragment"/>
    /// </summary>
    public interface IApplicationStructureFragmentBuilder
    {
        /// <summary>
        /// Gets the fragment built
        /// </summary>
        IApplicationStructureFragment Fragment { get; }

        /// <summary>
        /// Indicate the fragment is required
        /// </summary>
        /// <returns>Continued builder</returns>
        IApplicationStructureFragmentBuilder Required { get; }

        /// <summary>
        /// Indicate the fragment is recursive
        /// </summary>
        /// <returns>Continued builder</returns>
        IApplicationStructureFragmentBuilder Recursive { get; }

        /// <summary>
        /// Append a child fragment
        /// </summary>
        /// <returns>Continued builder</returns>
        IApplicationStructureFragmentBuilder WithChild<T>() where T : IApplicationLocationSegment;

        /// <summary>
        /// Append a child fragment
        /// </summary>
        /// <param name="callback">Child builder</param>
        /// <returns>Continued builder</returns>
        IApplicationStructureFragmentBuilder WithChild<T>(Func<IApplicationStructureFragmentBuilder, IApplicationStructureFragmentBuilder> callback)where T : IApplicationLocationSegment;
    }
}