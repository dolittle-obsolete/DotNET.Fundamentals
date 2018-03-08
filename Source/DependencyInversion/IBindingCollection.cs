/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Defines a collection for holding bindings
    /// </summary>
    public interface IBindingCollection : IEnumerable<Binding>
    {
        /// <summary>
        /// Check if there is a binding for a specific type by generic parameter
        /// </summary>
        /// <returns>True if there is a binding, false if not</returns>
        bool HasBindingFor<T>();

        /// <summary>
        /// Check if there is a binding for a specific type
        /// </summary>
        /// <returns>True if there is a binding, false if not</returns>
        bool HasBindingFor(Type type);
    }
}