/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dolittle.Collections;
using Dolittle.Logging;

namespace Dolittle.Types
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsSerializer"/>
    /// </summary>
    public class ContractToImplementorsSerializer : IContractToImplementorsSerializer
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ContractToImplementorsSerializer"/>
        /// </summary>
        /// <param name="logger"></param>
        public ContractToImplementorsSerializer(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public string Serialize(IDictionary<Type, IEnumerable<Type>> map)
        {
            var builder = new StringBuilder();
            map.ForEach(keyValue =>
            {
                var contract = keyValue.Key.AssemblyQualifiedName;
                if (string.IsNullOrEmpty(contract))
                {
                    if( keyValue.Key.IsGenericType )
                        contract = $"{keyValue.Key.Namespace}.{keyValue.Key.Name}, {keyValue.Key.Assembly.GetName().FullName}";
                    else
                        contract = $"{keyValue.Key}, {keyValue.Key.Assembly.GetName().FullName}";
                }
                builder.Append($"{contract}:");
                var first = true;
                keyValue.Value.ForEach(implementor =>
                {
                    if (!first) builder.Append(";");
                    first = false;
                    builder.Append(implementor.AssemblyQualifiedName);
                });
                builder.Append("\n");
            });

            return builder.ToString();
        }

        /// <inheritdoc/>
        public IDictionary<Type, IEnumerable<Type>> Deserialize(string serializedMap)
        {
            var map = new Dictionary<Type, IEnumerable<Type>>();

            var lines = serializedMap.Split('\n');
            var contractsCount = 0;
            var implementorsCount = 0;
            lines.ForEach(line =>
            {
                var keyValue = line.Split(':');
                var contract = Type.GetType(keyValue[0]);

                if (contract != null)
                {
                    var implementors = keyValue[1]
                        .Split(';')
                        .Select(_ => Type.GetType(_))
                        .Where(_ => _ != null)
                        .ToArray();
                    if (implementors.Length > 0)
                    {
                        map[contract] = implementors;
                        contractsCount++;
                        implementorsCount += implementors.Length;
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
    }
}