/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Assemblies;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the result of the <see cref="Bootloader"/> start
    /// </summary>
    public class BootloaderResult
    {
        /// <summary>
        /// Initialize a new instanace of <see cref="BootloaderResult"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> configured</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> configured</param>
        /// <param name="assemblies"><see cref="IAssemblies"/> configured</param>
        /// <param name="bindings"><see cref="IBindingCollection"/> configured</param>
        public BootloaderResult(
            IContainer container, 
            ITypeFinder typeFinder, 
            IAssemblies assemblies, 
            IBindingCollection bindings)
        {
            Container = container;
            TypeFinder = typeFinder;
            Assemblies = assemblies;
            Bindings = bindings;
        }

        /// <summary>
        /// Gets the <see cref="IContainer"/> configured
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Gets the <see cref="ITypeFinder"/> configured
        /// </summary>
        public ITypeFinder TypeFinder {  get; }

        /// <summary>
        /// Gets the <see cref="IAssemblies"/> configured
        /// </summary>
        public IAssemblies Assemblies {  get; }

        /// <summary>
        /// Gets the <see cref="IBindingCollection">bindings</see> configured
        /// </summary>
        public IBindingCollection Bindings {  get; }
    }
}