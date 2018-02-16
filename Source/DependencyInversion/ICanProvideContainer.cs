/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.DependencyInversion
{
    /// <summary>
    /// Defines a system that provide a <see cref="IContainer"/> implementation
    /// </summary>
    public interface ICanProvideContainer
    {
        /// <summary>
        /// Provide the container prebuilt with the given bindings
        /// </summary>
        /// <param name="bindings"><see cref="IBindingCollection">Bindings</see> provided</param>
        /// <returns><see cref="IContainer"/></returns>
        IContainer Provide(IBindingCollection bindings);
    }
}