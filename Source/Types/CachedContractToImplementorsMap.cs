/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dolittle.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class CachedContractToImplementorsMap : IContractToImplementorsMap
    {
        readonly IDictionary<Type, IEnumerable<Type>>   _map;
        readonly IEnumerable<Type> _all;


        /// <summary>
        /// The name of the embedded resource that holds the cache for the type map
        /// </summary>
        public const string MapResourceName = "ContractToImplementorsMap.Map";

        /// <summary>
        /// The name of the embedded resource that holds the cache for the types
        /// </summary>
        public const string TypesResourceName = "ContractToImplementorsMap.Types";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool HasCachedMap(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            return
                resourceNames.Contains(MapResourceName) &&
                resourceNames.Contains(TypesResourceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="assembly"></param>
        public CachedContractToImplementorsMap(IContractToImplementorsSerializer serializer, Assembly assembly)
        {
            using( var resourceStream = assembly.GetManifestResourceStream(MapResourceName) ) 
            {
                using( var reader = new StreamReader(resourceStream))
                {
                    var serialized = reader.ReadToEnd();
                    _map = serializer.DeserializeMap(serialized);
                }
            }

            using( var resourceStream = assembly.GetManifestResourceStream(TypesResourceName) ) 
            {
                using( var reader = new StreamReader(resourceStream))
                {
                    var serialized = reader.ReadToEnd();
                    _all = serializer.DeserializeTypes(serialized);
                }
            }
        }


        /// <inheritdoc/>
        public IDictionary<Type, IEnumerable<Type>> ContractsAndImplementors => _map;

        /// <inheritdoc/>
        public IEnumerable<Type> All => _all;

        /// <inheritdoc/>
        public void Feed(IEnumerable<Type> types)
        {
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

        IEnumerable<Type> GetImplementingTypesFor(Type contract)
        {
            IEnumerable<Type> implementingTypes;
            if( _map.ContainsKey(contract)) implementingTypes = _map[contract];
            else
            {
                implementingTypes = new Type[0];
                _map[contract] = implementingTypes;
            }
            return implementingTypes;
        }
    }
}