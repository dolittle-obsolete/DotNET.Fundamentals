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
using Dolittle.Scheduling;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsMap"/>
    /// </summary>
    public class ContractToImplementorsMap : IContractToImplementorsMap
    {
        readonly IScheduler _scheduler;
        readonly ConcurrentDictionary<Type, ConcurrentBag<Type>> _contractsAndImplementors = new ConcurrentDictionary<Type, ConcurrentBag<Type>>();
        readonly ConcurrentDictionary<Type, Type> _allTypes = new ConcurrentDictionary<Type, Type>();
        

        /// <summary>
        /// Initializes a new instance of <see cref="ContractToImplementorsMap"/>
        /// </summary>
        /// <param name="scheduler"></param>
        public ContractToImplementorsMap(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        /// <inheritdoc/>
        public IDictionary<Type, IEnumerable<Type>>    ContractsAndImplementors => _contractsAndImplementors.ToDictionary(_ => _.Key, _ => _.Value.AsEnumerable());

        /// <inheritdoc/>
        public IEnumerable<Type> All => _allTypes.Keys;

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
            return implementingTypes;
        }

        void AddTypesToAllTypes(IEnumerable<Type> types)
        {
            foreach( var type in types ) _allTypes[type] = type; 
        }

        void MapTypes(IEnumerable<Type> types)
        {
            var implementors = types.Where(IsImplementation);
            _scheduler.PerformForEach(implementors, implementor => 
            {
                var baseAndImplementingTypes = implementor.AllBaseAndImplementingTypes();
                foreach( var contract in baseAndImplementingTypes ) 
                {
                    var implementingTypes = GetImplementingTypesFor(contract);
                    if( !implementingTypes.Contains(implementor)) implementingTypes.Add(implementor);
                }
            });
        }

        bool IsImplementation(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsInterface && !typeInfo.IsAbstract;
        }

        ConcurrentBag<Type> GetImplementingTypesFor(Type contract)
        {
            var implementingTypes = _contractsAndImplementors.GetOrAdd(contract, (key) => new ConcurrentBag<Type>());
            return implementingTypes;
        }

        string GetKeyFor(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}