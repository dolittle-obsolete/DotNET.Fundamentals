/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Grpc.Core;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Defines a system representing all bound <see cref="ServerServiceDefinition">services</see> in the system
    /// </summary>
    public interface IBoundServices
    {
        /// <summary>
        /// Register all services for a specific type - this overwrites any that are already set
        /// </summary>
        /// <param name="type"><see cref="HostType"/> to register for </param>
        /// <param name="services">Collection of <see cref="ServerServiceDefinition"/></param>
        void Register(HostType type, IEnumerable<ServerServiceDefinition> services);

        /// <summary>
        /// Get all <see cref="ServerServiceDefinition"/> for a specific <see cref="HostType"/>
        /// </summary>
        /// <param name="type"><see cref="HostType"/> to get for</param>
        /// <returns>Collection of <see cref="ServerServiceDefinition"/></returns>
        IEnumerable<ServerServiceDefinition> GetFor(HostType type);
    }
}