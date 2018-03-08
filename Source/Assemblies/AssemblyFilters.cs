/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies.Configuration;

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
        public bool ShouldInclude(string filename)
        {
            return _assembliesConfiguration.Specification.IsSatisfiedBy(filename);
        }
    }
}
