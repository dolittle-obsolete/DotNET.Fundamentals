/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Defines a system for filtering assemblies
    /// </summary>
    public interface IAssemblyFilters
    {
        /// <summary>
        /// Method that decides wether or not an assembly should be included
        /// </summary>
        /// <param name="library">Library description</param>
        /// <returns>True if it should be included, false if not</returns>
        bool ShouldInclude(Library library);
    }
}
