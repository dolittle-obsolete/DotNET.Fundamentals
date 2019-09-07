/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Services
{
    /// <summary>
    /// Defines a system that manages all the <see cref="IEndpoint">hosts</see>
    /// </summary>
    public interface IEndpoints : IDisposable
    {
        /// <summary>
        /// Start all the hosts
        /// </summary>
        void Start();

        /// <summary>
        /// Get all the hosts set up in the process
        /// </summary>
        /// <returns>Collection of <see cref="EndpointInfo"/></returns>
        IEnumerable<EndpointInfo> GetEndpoints();
    }
}