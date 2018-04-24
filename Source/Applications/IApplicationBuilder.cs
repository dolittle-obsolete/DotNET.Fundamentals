/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a builder for building <see cref="IApplication"/> instances
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// Define what should be prefixed for application identifiers
        /// </summary>
        /// <param name="prefixes"></param>
        /// <returns><see cref="IApplicationBuilder"/> to continue building</returns>
        IApplicationBuilder PrefixLocationsWith(params IApplicationLocationSegment[] prefixes);

        /// <summary>
        /// Start building the structure
        /// </summary>
        /// <param name="fragmentBuilderCallback">Callback for building the <see cref="IApplicationStructureFragment"/></param>
        /// <param name="structureBuilderCallback">Optional <see cref="Action"/> that gets called with the <see cref="IApplicationStructureBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> to continue building</returns>
        /// <typeparam name="TFragment">Type of <see cref="IApplicationLocationSegment"/> to start with</typeparam>
        IApplicationBuilder WithStructureStartingWith<TFragment>(Func<IApplicationStructureFragmentBuilder, IApplicationStructureFragmentBuilder> fragmentBuilderCallback, Action<IApplicationStructureBuilder> structureBuilderCallback=null) where TFragment : IApplicationLocationSegment;

        /// <summary>
        /// Build an <see cref="IApplication">application definition</see>
        /// </summary>
        /// <returns>A instance of <see cref="IApplication"/></returns>
        IApplication  Build();
    }
}
