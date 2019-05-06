/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Dolittle.Lifecycle;
using Mono.Cecil;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="ITargetAssemblyModifiers"/>
    /// </summary>
    [Singleton]
    public class TargetAssemblyModifiers : ITargetAssemblyModifiers
    {
        readonly List<ICanModifyTargetAssembly> _modifiers = new List<ICanModifyTargetAssembly>();
        readonly BuildTarget _configuration;
        readonly IBuildMessages _buildMessages;

        /// <summary>
        /// Initializes a new instance of <see cref="TargetAssemblyModifiers"/>
        /// </summary>
        /// <param name="configuration"><see cref="BuildTarget"/> to use</param>
        /// <param name="buildMessages"><see cref="IBuildMessages"/> for build messages</param>
        public TargetAssemblyModifiers(
            BuildTarget configuration,
            IBuildMessages buildMessages)
        {
            _configuration = configuration;
            _buildMessages = buildMessages;
        }

        /// <inheritdoc/>
        public void AddModifier(ICanModifyTargetAssembly modifier)
        {
            _modifiers.Add(modifier);
        }

        /// <inheritdoc/>
        public void ModifyAndSave()
        {
            if (_modifiers.Count == 0) return;

            _buildMessages.Information("Performing assembly modifications");

            var tempFile = Path.GetTempFileName();

            _buildMessages.Indent();

            using(var stream = File.OpenRead(_configuration.TargetAssemblyPath))
            {
                using(var assemblyDefinition = AssemblyDefinition.ReadAssembly(stream))
                {
                    _modifiers.ForEach(_ =>
                    {
                        _buildMessages.Information($"{_.Message} (Modifier: '{_.GetType().AssemblyQualifiedName}')");
                        _buildMessages.Indent();
                        _.Modify(assemblyDefinition);
                        _buildMessages.Unindent();
                    });
                    assemblyDefinition.Write(tempFile);
                }
            }

            _buildMessages.Unindent();

            File.Delete(_configuration.TargetAssemblyPath);
            File.Move(tempFile, _configuration.TargetAssemblyPath);
        }
    }
}