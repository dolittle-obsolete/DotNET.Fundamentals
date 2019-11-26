// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsMap"/>.
    /// </summary>
    public class CachedContractToImplementorsMap : IContractToImplementorsMap
    {
        /// <summary>
        /// The name of the embedded resource that holds the cache for the type map.
        /// </summary>
        public const string MapResourceName = "ContractToImplementorsMap.Map";

        /// <summary>
        /// The name of the embedded resource that holds the cache for the types.
        /// </summary>
        public const string TypesResourceName = "ContractToImplementorsMap.Types";

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedContractToImplementorsMap"/> class.
        /// </summary>
        /// <param name="serializer"><see cref="IContractToImplementorsSerializer"/> for serialization.</param>
        /// <param name="assembly"><see cref="Assembly"/> that holds the cache.</param>
        public CachedContractToImplementorsMap(IContractToImplementorsSerializer serializer, Assembly assembly)
        {
            var before = DateTime.UtcNow;

            using (var resourceStream = assembly.GetManifestResourceStream(MapResourceName))
            {
                var bytes = new byte[resourceStream.Length];
                resourceStream.Read(bytes, 0, bytes.Length);
                var serialized = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                ContractsAndImplementors = serializer.DeserializeMap(serialized);
            }

            using (var resourceStream = assembly.GetManifestResourceStream(TypesResourceName))
            {
                var bytes = new byte[resourceStream.Length];
                resourceStream.Read(bytes, 0, bytes.Length);
                var serialized = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                All = serializer.DeserializeTypes(serialized);
            }

            Console.WriteLine($"Deserialization : {DateTime.UtcNow.Subtract(before).ToString("G", CultureInfo.InvariantCulture)}");
        }

        /// <inheritdoc/>
        public IDictionary<Type, IEnumerable<Type>> ContractsAndImplementors { get; }

        /// <inheritdoc/>
        public IEnumerable<Type> All { get; }

        /// <summary>
        /// Check if an assembly has the cached map or not.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> to check.</param>
        /// <returns>True if it has a cached map, false if not.</returns>
        public static bool HasCachedMap(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            return
                resourceNames.Contains(MapResourceName) &&
                resourceNames.Contains(TypesResourceName);
        }

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
            if (ContractsAndImplementors.ContainsKey(contract))
            {
                implementingTypes = ContractsAndImplementors[contract];
            }
            else
            {
                implementingTypes = Array.Empty<Type>();
                ContractsAndImplementors[contract] = implementingTypes;
            }

            return implementingTypes;
        }
    }
}