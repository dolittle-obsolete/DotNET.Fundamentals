/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Defines a system that can provide assemblies
    /// </summary>
    public interface ICanProvideAssemblies
    {
        /// <summary>
        /// Get available assemblies that can be provided
        /// </summary>
        /// <returns></returns>
        IEnumerable<Library> Libraries { get; }

        /// <summary>
        /// Get a specific assembly based on a <see cref="Library"/> representation
        /// </summary>
        /// <param name="library"><see cref="Library"/> representing the <see cref="Assembly"/></param>
        /// <returns>Loaded <see cref="Assembly"/></returns>
        Assembly GetFrom(Library library);
    }
}
