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
        /// 
        /// </summary>
        /// <param name="service"></param>
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