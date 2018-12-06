/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.DependencyInversion.Booting
{
    /// <summary>
    /// Represents the result of booting
    /// </summary>
    public class BootResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BootResult"/>
        /// </summary>
        /// <param name="container">Configured <see cref="IContainer"/></param>
        /// <param name="bindings">Configured <see cref="IBindingCollection">bindings</see></param>
        public BootResult(IContainer container, IBindingCollection bindings)
        {
            Container = container;
            Bindings = bindings;
        }

        /// <summary>
        /// Gets the <see cref="IContainer"/> configured
        /// </summary>
        public IContainer Container { get; }

        /// <summary>
        /// Gets the <see cref="IBindingCollection">bindings</see> set up during boot
        /// </summary>
        public IBindingCollection Bindings { get; }
    }
}