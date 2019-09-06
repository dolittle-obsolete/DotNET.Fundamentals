/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Services
{
    /// <summary>
    /// Defines a system representing all bound <see cref="Service">services</see> in the system
    /// </summary>
    public interface IBoundServices
    {
        /// <summary>
        /// Register all services for a specific type - this overwrites any that are already set
        /// </summary>
        /// <param name="type"><see cref="HostType"/> to register for </param>
        /// <param name="services">Collection of <see cref="Service"/></param>
        void Register(HostType type, IEnumerable<Service> services);

        /// <summary>
        /// Check if there are bound services for a specific <see cref="HostType"/>
        /// </summary>
        /// <param name="type"><see cref="HostType"/> to check if has services</param>
        /// <returns>True if there are services, false if not</returns>
        bool HasFor(HostType type);

        /// <summary>
        /// Get all <see cref="Service"/> for a specific <see cref="HostType"/>
        /// </summary>
        /// <param name="type"><see cref="HostType"/> to get for</param>
        /// <returns>Collection of <see cref="Service"/></returns>
        IEnumerable<Service> GetFor(HostType type);
    }
}