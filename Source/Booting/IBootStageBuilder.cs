/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Defines a builder for a <see cref="BootStage"/>
    /// </summary>
    public interface IBootStageBuilder
    {
        /// <summary>
        /// Gets the <see cref="IBindingProviderBuilder"/> for building specific 
        /// </summary>
        IBindingProviderBuilder Bindings { get; }

        /// <summary>
        /// Called to switch to a specific <see cref="IContainer"/> - any stage beyond this stage will use the <see cref="IContainer"/> specified
        /// </summary>
        void UseContainer(IContainer container);

        /// <summary>
        /// Associate a type with an instance
        /// </summary>
        /// <param name="instance">Instance of the type to associate with</param>
        /// <typeparam name="T">Type to associate</typeparam>
        void Associate<T>(T instance);

        /// <summary>
        /// Build the <see cref="BootStage"/> and return the <see cref="BootStageResult">result</see>
        /// </summary>
        /// <returns>Resulting <see cref="BootStageResult"/></returns>
        BootStageResult Build();
    }
}