/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a <see cref="ICompilationAssemblyResolver"/> that can run through multiple resolvers
    /// </summary>
    public class CompositeAssemblyResolver : ICompilationAssemblyResolver
    {
        readonly ICompilationAssemblyResolver[] _resolvers;

        /// <summary>
        /// Initializes a new instance of <see cref="CompositeAssemblyResolver"/>
        /// </summary>
        /// <param name="resolvers">Params of <see cref="ICompilationAssemblyResolver">resolvers</see></param>
        public CompositeAssemblyResolver(params ICompilationAssemblyResolver[] resolvers)
        {
            _resolvers = resolvers;
        }

        /// <inheritdoc/>
        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {
            var found = false;

            foreach( var resolver in _resolvers )
            {
                try
                {
                    found |= resolver.TryResolveAssemblyPaths(library, assemblies);
                } catch { }
            }
            
            return found;
        }
    }
}