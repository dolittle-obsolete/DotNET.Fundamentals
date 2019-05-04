/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dolittle.Assemblies;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Scheduling;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsSerializer"/>
    /// </summary>
    public class ContractToImplementorsSerializer : IContractToImplementorsSerializer
    {
        readonly ILogger _logger;
        readonly ConcurrentDictionary<string, Type> _typesByName = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Initializes a new instance of <see cref="ContractToImplementorsSerializer"/>
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="assemblies"></param>
        /// <param name="logger"></param>
        public ContractToImplementorsSerializer(
            IScheduler scheduler,
            IAssemblies assemblies,
            ILogger logger)
        {
            _logger = logger;

            var before = DateTime.UtcNow;

            scheduler.PerformForEach(assemblies.GetAll(), assembly =>
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (!string.IsNullOrEmpty(type.AssemblyQualifiedName)) _typesByName[type.AssemblyQualifiedName] = type;
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        _logger.Error($"Failed to load: {loaderException.Source} {loaderException.Message}");
                }

            });
            Console.WriteLine($"Gathering Types : {DateTime.UtcNow.Subtract(before).ToString("G")}");
        }

        /// <inheritdoc/>
        public string SerializeMap(IDictionary<Type, IEnumerable<Type>> map)
        {
            var builder = new StringBuilder();
            map.ForEach(keyValue =>
            {
                var contract = GetAssemblyQualifiedNameFor(keyValue.Key);
                builder.Append($"{contract}:");
                var first = true;
                keyValue.Value.ForEach(implementor =>
                {
                    if (!first) builder.Append(";");
                    first = false;
                    builder.Append(GetAssemblyQualifiedNameFor(implementor));
                });
                builder.Append("\n");
            });

            return builder.ToString();
        }

        /// <inheritdoc/>
        public string SerializeTypes(IEnumerable<Type> types)
        {
            var builder = new StringBuilder();
            types.ForEach(_ =>
            {
                builder.Append(GetAssemblyQualifiedNameFor(_));
                builder.Append("\n");
            });
            return builder.ToString();
        }

        /// <inheritdoc/>
        public IDictionary<Type, IEnumerable<Type>> DeserializeMap(string serializedMap)
        {
            var map = new Dictionary<Type, IEnumerable<Type>>();

            var lines = serializedMap.Split('\n');
            var contractsCount = 0;
            var implementorsCount = 0;
            lines.ForEach(line =>
            {
                var keyValue = line.Split(':');

                if (_typesByName.ContainsKey(keyValue[0]))
                {
                    var contract = _typesByName[keyValue[0]];
                    var implementorTypeNames = keyValue[1].Split(';');
                    var implementors = new List<Type>();
                    implementorTypeNames.ForEach(implementorTypeName =>
                    {
                        if (_typesByName.ContainsKey(implementorTypeName))
                            implementors.Add(_typesByName[implementorTypeName]);
                    });

                    if (implementors.Count > 0)
                    {
                        map[contract] = implementors;
                        contractsCount++;
                        implementorsCount += implementors.Count;
                    }
                    else
                    {
                        _logger.Information($"No implementations of '{keyValue[0]}'");
                    }
                }
                else
                {
                    _logger.Information($"Can't find contract type '{keyValue[0]}' - {line}");
                }
            });

            _logger.Information($"Using {contractsCount} contracts mapped to {implementorsCount} implementors in total");

            return map;
        }

        /// <inheritdoc/>
        public IEnumerable<Type> DeserializeTypes(string serializedTypes)
        {
            var types = new List<Type>();
            var lines = serializedTypes.Split('\n');
            lines.ForEach(line =>
            {
                if(_typesByName.ContainsKey(line)) types.Add(_typesByName[line]);
            });
            return types;
        }

        string GetAssemblyQualifiedNameFor(Type type)
        {
            var name = type.AssemblyQualifiedName;
            if (string.IsNullOrEmpty(name))
            {
                if (type.IsGenericType)
                    name = $"{type.Namespace}.{type.Name}, {type.Assembly.GetName().FullName}";
                else
                    name = $"{type}, {type.Assembly.GetName().FullName}";
            }
            return name;
        }
    }
}