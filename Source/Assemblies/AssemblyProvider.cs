/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace doLittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyProvider"/>
    /// </summary>
    public class AssemblyProvider : IAssemblyProvider
    {
        /// <inheritdoc/>
        public IEnumerable<AssemblyName> GetAllByName()
        {
            var assemblies = new List<Assembly>();
            
            var entryAssembly = Assembly.GetEntryAssembly();
            var dependencyModel = DependencyContext.Load(entryAssembly);
            var assemblyNames = dependencyModel.GetRuntimeAssemblyNames(RuntimeEnvironment.GetRuntimeIdentifier());
            return assemblyNames;
        }

        /// <inheritdoc/>
        public Assembly Load(AssemblyName name)
        {
            return Assembly.Load(name);
        }
    }
}