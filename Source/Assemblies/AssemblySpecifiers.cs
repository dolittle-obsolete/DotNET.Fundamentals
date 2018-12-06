/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Dolittle.Assemblies.Configuration;
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Reflection;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblySpecifiers"/>
    /// </summary>
    public class AssemblySpecifiers : IAssemblySpecifiers
    {
        readonly IAssemblyRuleBuilder _assemblyRuleBuilder;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblySpecifiers"/>
        /// </summary>
        /// <param name="assemblyRuleBuilder"><see cref="IAssemblyRuleBuilder"/> used for building the rules for assemblies</param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public AssemblySpecifiers(IAssemblyRuleBuilder assemblyRuleBuilder, ILogger logger)
        {
            _assemblyRuleBuilder = assemblyRuleBuilder;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void SpecifyUsingSpecifiersFrom(Assembly assembly)
        {
            assembly
                .GetTypes()
                .Where(t => t.Implements(typeof(ICanSpecifyAssemblies)))
                .Where(t => t.GetTypeInfo().Assembly.FullName == assembly.FullName)
                .Where(type => type.HasDefaultConstructor())
                .ForEach((Action<Type>)(type =>
                {
                    _logger.Trace((string)$"Specifying from type {type.AssemblyQualifiedName}");
                    var specifier = Activator.CreateInstance(type) as ICanSpecifyAssemblies;
                    specifier.Specify(_assemblyRuleBuilder);
                }));
        }
    }
}
