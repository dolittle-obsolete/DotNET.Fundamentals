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
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Scheduling;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeFinder"/>
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        readonly IAssemblies _assemblies;
        readonly IContractToImplementorsMap _contractToImplementorsMap;
        readonly IScheduler _scheduler;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeFinder"/>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for getting assemblies</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        /// <param name="scheduler"><see cref="IScheduler"/> for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public TypeFinder(
            IAssemblies assemblies,
            IContractToImplementorsMap contractToImplementorsMap,
            IScheduler scheduler,
            ILogger logger)
        {
            _assemblies = assemblies;
            _contractToImplementorsMap = contractToImplementorsMap;
            _scheduler = scheduler;
            _logger = logger;

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
            _scheduler.PerformForEach(assemblies, assembly => 
            {
                try
                {
                    var types = assembly.GetTypes();
                    _contractToImplementorsMap.Feed(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        _logger.Error($"Failed to load: {loaderException.Source} {loaderException.Message}");
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
