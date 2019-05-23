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
    /// Represents an implementation of <see cref="IContractToImplementorsMap"/>
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
        /// Check if an assembly has the cached map or not
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> to check</param>
        /// <returns>True if it has a cached map, false if not</returns>
        public static bool HasCachedMap(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            return
                resourceNames.Contains(MapResourceName) &&
                resourceNames.Contains(TypesResourceName);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CachedContractToImplementorsMap"/>
        /// </summary>
        /// <param name="serializer"><see cref="IContractToImplementorsSerializer"/> for serialization</param>
        /// <param name="assembly"><see cref="Assembly"/> that holds the cache</param>
        public CachedContractToImplementorsMap(IContractToImplementorsSerializer serializer, Assembly assembly)
        {
            var before = DateTime.UtcNow;
            
            using( var resourceStream = assembly.GetManifestResourceStream(MapResourceName) ) 
            {
                var bytes = new byte[resourceStream.Length];
                resourceStream.Read(bytes,0,bytes.Length);
                /*var chars = new char[bytes.Length];
                System.Buffer.BlockCopy(bytes,0,chars,0,bytes.Length);
                var serialized = new string(chars);*/

                //Console.WriteLine(serialized);
                var serialized = System.Text.Encoding.UTF8.GetString(bytes,0, bytes.Length);
                _map = serializer.DeserializeMap(serialized);
            }
            

            using( var resourceStream = assembly.GetManifestResourceStream(TypesResourceName) ) 
            {
                var bytes = new byte[resourceStream.Length];
                resourceStream.Read(bytes,0,bytes.Length);
                /*var chars = new char[bytes.Length];
                System.Buffer.BlockCopy(bytes,0,chars,0,bytes.Length);
                var serialized = new string(chars);*/
                var serialized = System.Text.Encoding.UTF8.GetString(bytes,0, bytes.Length);
                _all = serializer.DeserializeTypes(serialized);
            }
            Console.WriteLine($"Deserialization : {DateTime.UtcNow.Subtract(before).ToString("G")}");            
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