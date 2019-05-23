/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Lifecycle;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a <see cref="IAssemblies"/>
    /// </summary>
    [Singleton]
    public class Assemblies : IAssemblies
    {
        readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="Assemblies"/>
        /// </summary>
        public Assemblies(Assembly entryAssembly, IAssemblyProvider assemblyProvider)
        {   
            EntryAssembly = entryAssembly;
            _assemblies = assemblyProvider.GetAll();
        }

        /// <inheritdoc/>
        public Assembly EntryAssembly { get; }

        /// <inheritdoc/>
        public IEnumerable<Assembly> GetAll()
        {
            return _assemblies;
        }

        /// <inheritdoc/>
        public Assembly GetByFullName(string fullName)
        {
            var query = from a in _assemblies
                        where a.FullName == fullName
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }

        /// <inheritdoc/>
        public Assembly GetByName(string name)
        {
            var query = from a in _assemblies
                        where a.FullName.Contains(name)
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }
    }
}