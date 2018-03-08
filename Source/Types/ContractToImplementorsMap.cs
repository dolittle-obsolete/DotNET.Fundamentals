/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Reflection;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsMap"/>
    /// </summary>
    public class ContractToImplementorsMap : IContractToImplementorsMap
    {
        ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>> _contractsAndImplementors = new ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>>();
        ConcurrentDictionary<Type, Type> _allTypes = new ConcurrentDictionary<Type, Type>();

        /// <inheritdoc/>
        public IEnumerable<Type> All { get { return _allTypes.Keys; } }

        /// <inheritdoc/>
        public void Feed(IEnumerable<Type> types)
        {
            MapTypes(types);
            AddTypesToAllTypes(types);
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetImplementorsFor<T>()
        {
            return GetImplementorsFor(typeof(T));
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetImplementorsFor(Type contract)
        {
            var implementingTypes = GetImplementingTypesFor(contract);
            return implementingTypes.Values;
        }

        void AddTypesToAllTypes(IEnumerable<Type> types)
        {
            foreach( var type in types ) _allTypes[type] = type; 
        }

        void MapTypes(IEnumerable<Type> types)
        {
            var implementors = types.Where(IsImplementation);
            Parallel.ForEach(implementors, implementor =>
            {
                var baseAndImplementingTypes = implementor.AllBaseAndImplementingTypes();
                foreach( var contract in baseAndImplementingTypes ) GetImplementingTypesFor(contract)[GetKeyFor(implementor)] = implementor;
            });
        }

        bool IsImplementation(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsInterface && !typeInfo.IsAbstract;
        }

        ConcurrentDictionary<string, Type> GetImplementingTypesFor(Type contract)
        {
            var implementingTypes = _contractsAndImplementors.GetOrAdd(contract, (key) => new ConcurrentDictionary<string, Type>());
            return implementingTypes;
        }

        string GetKeyFor(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}