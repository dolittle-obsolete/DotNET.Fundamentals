/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Collections;
using Dolittle.Logging;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents a <see cref="ICanProvideAssemblies"/> for providing all the default assemblies plus plugins
    /// </summary>
    public class AssemblyProvider : ICanProvideAssemblies
    {
        readonly DefaultAssemblyProvider _innerProvider;
        readonly List<Library> _libraries = new List<Library>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="pluginAssemblies"></param>
        public AssemblyProvider(ILogger logger, IEnumerable<string> pluginAssemblies)
        {
            _innerProvider = new DefaultAssemblyProvider(logger);
            _libraries.AddRange(_innerProvider.Libraries);

            pluginAssemblies.ForEach(pluginAssembly => 
            {
                var assembly = Assembly.LoadFrom(pluginAssembly);
                var assemblyName = assembly.GetName();
                var library = new Library(
                    "Project", 
                    assemblyName.Name, 
                    assemblyName.Version.ToString(), 
                    string.Empty,
                    new Dependency[0],
                    false,
                    pluginAssembly,
                    string.Empty);
                _libraries.Add(library);
            });
        }

        /// <inheritdoc/>
        public IEnumerable<Library> Libraries => _libraries;

        /// <inheritdoc/>
        public Assembly GetFrom(Library library)
        {
            return _innerProvider.GetFrom(library);
        }
    }
}