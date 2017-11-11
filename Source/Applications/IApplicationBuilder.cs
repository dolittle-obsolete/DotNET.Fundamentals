/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace doLittle.Applications
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
        IApplicationBuilder PrefixedWith(params IApplicationLocationFragment[] prefixes);

        /// <summary>
        /// Start building the structure
        /// </summary>
        /// <param name="fragment">Fragment to start with</param>
        /// <param name="structureBuilderCallback"><see cref="Action"/> that gets called with the <see cref="IApplicationStructureBuilder"/></param>
        /// <returns><see cref="IApplicationBuilder"/> to continue building</returns>
        /// <typeparam name="TFragment">Type of <see cref="IApplicationStructureFragment"/></typeparam>
        IApplicationBuilder WithStructureStartingWith<TFragment>(TFragment fragment, Action<IApplicationStructureBuilder> structureBuilderCallback) where TFragment:IApplicationStructureFragment;

        /// <summary>
        /// Build an <see cref="IApplication">application definition</see>
        /// </summary>
        /// <returns>A instance of <see cref="IApplication"/></returns>
        IApplication  Build();
    }
}
