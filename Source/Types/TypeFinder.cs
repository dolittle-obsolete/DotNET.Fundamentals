/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Assemblies;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeFinder"/>
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        IAssemblies _assemblies;
        IContractToImplementorsMap _contractToImplementorsMap;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeFinder"/>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for getting assemblies</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        public TypeFinder(IAssemblies assemblies, IContractToImplementorsMap contractToImplementorsMap)
        {
            _assemblies = assemblies;
            _contractToImplementorsMap = contractToImplementorsMap;

            CollectTypes();
        }

        /// <inheritdoc/>
        public IEnumerable<Type> All => _contractToImplementorsMap.All;
        
        /// <inheritdoc/>
        public Type FindSingle<T>()
        {
            var type = FindSingle(typeof(T));
            return type;
        }

        /// <inheritdoc/>
        public IEnumerable<Type> FindMultiple<T>()
        {
            var typesFound = FindMultiple(typeof(T));
            return typesFound;
        }

        /// <inheritdoc/>
        public Type FindSingle(Type type)
        {
            var typesFound = _contractToImplementorsMap.GetImplementorsFor(type);
            ThrowIfMultipleTypesFound(type, typesFound);
            return typesFound.SingleOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<Type> FindMultiple(Type type)
        {
            var typesFound = _contractToImplementorsMap.GetImplementorsFor(type);
            return typesFound;
        }

        /// <inheritdoc/>
        public Type FindTypeByFullName(string fullName)
        {
            var typeFound = _contractToImplementorsMap.All.Where(t => t.FullName == fullName).SingleOrDefault();
            ThrowIfTypeNotFound(fullName, typeFound);
            return typeFound;
        }

        void CollectTypes()
        {
            var assemblies = _assemblies.GetAll();
            Parallel.ForEach(assemblies, assembly =>
            {
                try
                {
                    var types = assembly.GetTypes();
                    _contractToImplementorsMap.Feed(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        Debug.WriteLine(string.Format("Failed to load: {0} {1}", loaderException.Source, loaderException.Message));
                }
            });
        }

        void ThrowIfMultipleTypesFound(Type type, IEnumerable<Type> typesFound)
        {
            if (typesFound.Count() > 1)
                throw new MultipleTypesFoundException(string.Format("More than one type found for '{0}'", type.FullName));
        }

        void ThrowIfTypeNotFound(string fullName, Type typeFound)
        {
            if (typeFound == null) throw new UnableToResolveTypeByName(fullName);
        }
    }
}
