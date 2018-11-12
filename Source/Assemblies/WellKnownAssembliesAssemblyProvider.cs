/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a <see cref="ICanProvideAssemblies">assembly provider</see> that will provide only well known assemblies
    /// </summary>
    public class WellKnownAssembliesAssemblyProvider : ICanProvideAssemblies
    {
        /// <inheritdoc/>
        public IEnumerable<Library> Libraries { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="WellKnownAssembliesAssemblyProvider"/>
        /// </summary>
        /// <param name="assemblies"><see cref="IEnumerable{T}">Collection</see> of <see cref="AssemblyName"/> with all known assemblies</param>
        public WellKnownAssembliesAssemblyProvider(IEnumerable<AssemblyName> assemblies)
        {
            Libraries = assemblies.Select(_ =>
                new Library("Package", _.Name, _.Version.ToString(), string.Empty, new Dependency[0], false));
        }

        /// <inheritdoc/>
        public Assembly GetFrom(Library library)
        {
            return Assembly.Load(library.Name);
        }
    }
}