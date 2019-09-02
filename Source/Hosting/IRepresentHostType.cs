/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Defines a system that can provide information about a host type
    /// </summary>
    public interface IRepresentHostType
    {
        /// <summary>
        /// Gets the identifier of the <see cref="HostType"/>
        /// </summary>
        HostType    Identifier { get; }

        /// <summary>
        /// Gets the binding interface, must implement <see cref="ICanBindServices"/>
        /// </summary>
        Type BindingInterface { get; }

        /// <summary>
        /// Gets the host configuration object
        /// </summary>
        HostConfiguration Configuration { get; }        
    }
}