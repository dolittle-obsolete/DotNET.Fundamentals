/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.DependencyInversion
{
    /// <summary>
    /// Represents the context for a binding, typically used in callbacks that resolve instance or type
    /// </summary>
    public class BindingContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BindingContext"/>
        /// </summary>
        /// <param name="service"><see cref="Type">Service type</see> the context is for</param>
        public BindingContext(Type service)
        {
            Service = service;
        }

        /// <summary>
        /// Gets the service being asked for
        /// </summary>
        public Type Service { get; }
    }
}