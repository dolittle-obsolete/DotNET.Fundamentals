/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies.Configuration;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyFilters"/>
    /// </summary>
    public class AssemblyFilters : IAssemblyFilters
    {
        AssembliesConfiguration _assembliesConfiguration;

        /// <summary>
        /// Initializes an instance of <see cref="AssemblyFilters"/>
        /// </summary>
        /// <param name="assembliesConfiguration"></param>
        public AssemblyFilters(AssembliesConfiguration assembliesConfiguration)
        {
            _assembliesConfiguration = assembliesConfiguration;
        }

        /// <inheritdoc/>
        public bool ShouldInclude(Library library)
        {
            return _assembliesConfiguration.Specification.IsSatisfiedBy(library);
        }
    }
}
