/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// Defines a system that can provide <see cref="Binding">bindings</see> critical for booting
    /// </summary>
    public interface ICanProvideBootBindings
    {
        /// <summary>
        /// Method that gets called to provide bindings
        /// </summary>
        /// <param name="builder">The <see cref="IBindingBuilder"/> to use for building and providing bindings</param>
        void Provide(IBindingProviderBuilder builder);
    }
}