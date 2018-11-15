/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a system that holds information about an assembly's context
    /// </summary>
    public interface IAssemblyContext : IDisposable
    {
         /// <summary>
        /// Gets the loaded root assembly
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the <see cref="DependencyContext"/> for the <see cref="Assembly"/>
        /// </summary>
        DependencyContext DependencyContext {  get; }

        /// <summary>
        /// Gets the <see cref="AssemblyLoadContext"/> for the <see cref="Assembly"/>
        /// </summary>
        AssemblyLoadContext AssemblyLoadContext {  get; }

        /// <summary>
        /// Get all assemblies that are referenced by the assembly
        /// </summary>
        /// <returns>All references <see cref="IEnumerable{Assembly}">assemblies</see></returns>
        IEnumerable<Assembly> GetReferencedAssemblies();

        /// <summary>
        /// Get assemblies that are referenced as project referenced by the assembly
        /// </summary>
        /// <returns>Project <see cref="IEnumerable{Assembly}">assemblies</see></returns>
        IEnumerable<Assembly> GetProjectReferencedAssemblies();
    }
}