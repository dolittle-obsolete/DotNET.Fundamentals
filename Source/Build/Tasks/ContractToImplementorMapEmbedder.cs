// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Assemblies;
using Dolittle.Assemblies.Configuration;
using Dolittle.Assemblies.Rules;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Serialization.Json;
using Dolittle.Types;

namespace Dolittle.Build.Tasks
{
#if(false)    
    /// <summary>
    /// Represents a <see cref="ICanPerformBuildTask"/> that can create a <see cref="IContractToImplementorsMap"/>
    /// </summary>
    public class ContractToImplementorMapEmbedder : ICanPerformBuildTask
    {
        readonly BuildTarget _buildTarget;
        readonly ITargetAssemblyModifiers _modifiers;
        readonly IContractToImplementorsSerializer _serializer;
        readonly IBuildMessages _buildMessages;
        readonly ILogger _logger;
        

        /// <summary>
        /// Initializes a new instance of <see cref="ContractToImplementorMapEmbedder"/>
        /// </summary>
        /// <param name="buildTarget"><see cref="BuildTarget"/> to embed for</param>
        /// <param name="modifiers"><see cref="ITargetAssemblyModifiers"/> for working with modifiers</param>
        /// <param name="serializer"><see cref="IContractToImplementorsSerializer"/> for serializating maps</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for outputting build messages</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public ContractToImplementorMapEmbedder(
            BuildTarget buildTarget,
            ITargetAssemblyModifiers modifiers,
            IContractToImplementorsSerializer serializer,
            IBuildMessages buildMessages,
            ILogger logger)
        {
            _buildTarget = buildTarget;
            _modifiers = modifiers;
            _buildMessages = buildMessages;
            _logger = logger;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public string Message => "Creating and embedding contract to implementor map";

        /// <inheritdoc/>
        public void Perform()
        {
            var assembliesConfigurationBuilder = new AssembliesConfigurationBuilder();
            assembliesConfigurationBuilder
                .ExcludeAll()
                .ExceptProjectLibraries()
                .ExceptDolittleLibraries();

            var entryAssembly = _buildTarget.Assembly;

            var assembliesConfiguration = new AssembliesConfiguration(assembliesConfigurationBuilder.RuleBuilder);
            var assemblyFilters = new AssemblyFilters(assembliesConfiguration);

            var assemblyProvider = new Dolittle.Assemblies.AssemblyProvider(
                new ICanProvideAssemblies[] { new DefaultAssemblyProvider(_logger, entryAssembly) },
                assemblyFilters,
                new AssemblyUtility(),
                _logger
            );

            var assemblies = new Dolittle.Assemblies.Assemblies(entryAssembly, assemblyProvider);
            var contractToImplementorsMap = new ContractToImplementorsMap(new AsyncScheduler());
            
            Parallel.ForEach(assemblies.GetAll(), _ => 
            {
                try
                {
                    var types = _.GetTypes();
                    contractToImplementorsMap.Feed(types);
                }
                catch {}
            });

            var contractsToImplementors = contractToImplementorsMap.ContractsAndImplementors;

            var serializedMap = _serializer.SerializeMap(contractsToImplementors);
            _modifiers.AddModifier(new EmbedResource(CachedContractToImplementorsMap.MapResourceName, Encoding.UTF8.GetBytes(serializedMap)));
            var serializedTypes = _serializer.SerializeTypes(contractToImplementorsMap.All);
            _modifiers.AddModifier(new EmbedResource(CachedContractToImplementorsMap.TypesResourceName, Encoding.UTF8.GetBytes(serializedTypes)));

            var implementors = contractsToImplementors.Values.Sum(_ => _.Count());
            _buildMessages.Information($"Embedded a map with {contractsToImplementors.Keys.Count} contracts to {implementors} implementors");
        }
    }
#endif    
}