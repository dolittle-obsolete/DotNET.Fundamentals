/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Defines a system that manages all the <see cref="IHost">hosts</see>
    /// </summary>
    public interface IHosts : IDisposable
    {
        /// <summary>
        /// Start all the hosts
        /// </summary>
        void Start();

        /// <summary>
        /// Get all the hosts set up in the process
        /// </summary>
        /// <returns>Collection of <see cref="HostInfo"/></returns>
        IEnumerable<HostInfo> GetHosts();
    }
}