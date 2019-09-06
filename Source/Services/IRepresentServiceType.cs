/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Services
{
    /// <summary>
    /// Defines a system that can provide information about a service type
    /// </summary>
    public interface IRepresentServiceType
    {
        /// <summary>
        /// Gets the identifier of the <see cref="ServiceType"/>
        /// </summary>
        ServiceType    Identifier { get; }

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