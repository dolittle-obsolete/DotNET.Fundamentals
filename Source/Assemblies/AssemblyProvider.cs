/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Execution;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyProvider"/>
    /// </summary>
    [Singleton]
    public class AssemblyProvider : IAssemblyProvider
    {
        static object _lockObject = new object();
        readonly AssemblyComparer comparer = new AssemblyComparer();
        readonly IEnumerable<ICanProvideAssemblies> _assemblyProviders;
        readonly IAssemblyFilters _assemblyFilters;
        readonly IAssemblyUtility _assemblyUtility;
        readonly IAssemblySpecifiers _assemblySpecifiers;

        readonly Dictionary<string,Library> _libraries = new Dictionary<string,Library>();
        readonly List<Assembly> _assemblies = new List<Assembly>();

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyProvider"/>
        /// </summary>
        /// <param name="assemblyProviders"><see cref="IEnumerable{ICanProvideAssemblies}">Providers</see> to provide assemblies</param>
        /// <param name="assemblyFilters"><see cref="IAssemblyFilters"/> to use for filtering assemblies through</param>
        /// <param name="assemblyUtility">An <see cref="IAssemblyUtility"/></param>
        /// <param name="assemblySpecifiers"><see cref="IAssemblySpecifiers"/> used for specifying what assemblies to include or not</param>
        public AssemblyProvider(
            IEnumerable<ICanProvideAssemblies> assemblyProviders,
            IAssemblyFilters assemblyFilters, 
            IAssemblyUtility assemblyUtility,
            IAssemblySpecifiers assemblySpecifiers)
        {
            _assemblyProviders = assemblyProviders;
            _assemblyFilters = assemblyFilters;
            _assemblyUtility = assemblyUtility;
            _assemblySpecifiers = assemblySpecifiers;

            Populate();
        }

        /// <inheritdoc/>
        public IEnumerable<Assembly> GetAll()
        {
            return _assemblies;
        }

        void Populate()
        {
            foreach (var provider in _assemblyProviders)
            {
                provider.Libraries.ForEach(library => _libraries.Add(library.Name, library));

                var assembliesToInclude = provider.Libraries.Where(
                    library => 
                        _assemblyFilters.ShouldInclude(library) && 
                        _assemblyUtility.IsAssembly(library)
                    );

                var filtered = assembliesToInclude.ToArray();

                assembliesToInclude.Select(provider.GetFrom).ForEach(AddAssembly);
            }
        }

        void SpecifyRules(Assembly assembly)
        {
            _assemblySpecifiers.SpecifyUsingSpecifiersFrom(assembly);
        }

        void ReapplyFilter()
        {
            var assembliesToRemove = _assemblies.Where(a => {
                var name = a.GetName().Name;
                if( !_libraries.ContainsKey(name) ) return true;
                return !_assemblyFilters.ShouldInclude(_libraries[name]);
            }).ToArray();
            assembliesToRemove.ForEach((a) =>_assemblies.Remove(a));
        }

        void AddAssembly(Assembly assembly)
        {
            lock (_lockObject)
            {
                if (!_assemblies.Contains(assembly, comparer) &&
                    !_assemblyUtility.IsDynamic(assembly))
                {
                    _assemblies.Add(assembly);
                    SpecifyRules(assembly);
                    ReapplyFilter();
                }
            }
        }

        bool Matches(AssemblyName a, AssemblyName b)
        {
            return a.ToString() == b.ToString();
        }
    }
}
