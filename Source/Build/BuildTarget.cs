/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Assemblies;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents the configuration for the build
    /// </summary>
    public class BuildTarget
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BuildTarget"/>
        /// </summary>
        /// <param name="targetAssemblyPath">Path of the target assembly being built</param>
        /// <param name="assembly"><see cref="Assembly"/> being built</param>
        /// <param name="assemblyContext"><see cref="AssemblyContext"/> for the <see cref="Assembly"/> being built</param>
        public BuildTarget(
            string targetAssemblyPath,
            Assembly assembly,
            IAssemblyContext assemblyContext)
        {
            TargetAssemblyPath = targetAssemblyPath;
            Assembly = assembly;
            AssemblyContext = assemblyContext;
            AssemblyName = assembly.GetName();
        }

        /// <summary>
        /// Gets the path of the target assembly being build
        /// </summary>
        public string TargetAssemblyPath {  get; }

        /// <summary>
        /// Gets the <see cref="Assembly"/> being built
        /// </summary>
        public Assembly Assembly {  get; }

        /// <summary>
        /// Gets the <see cref="AssemblyContext"/> for the <see cref="Assembly"/> being built
        /// </summary>
        public IAssemblyContext AssemblyContext {  get; }

        /// <summary>
        /// Gets the <see cref="AssemblyName"/> for the <see cref="Assembly"/>
        /// </summary>
        public AssemblyName AssemblyName { get; }
    }
}