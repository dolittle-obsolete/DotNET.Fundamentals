/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Grpc.Core;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundServices"/>
    /// </summary>
    [Singleton]
    public class BoundServices : IBoundServices
    {
        readonly ConcurrentDictionary<HostType, IEnumerable<Service>>    _servicesPerHostType = new  ConcurrentDictionary<HostType, IEnumerable<Service>>();
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundServices"/>
        /// </summary>
        public BoundServices(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Register(HostType type, IEnumerable<Service> services)
        {
            _logger.Information($"  {services.Count()} will be added");
            _servicesPerHostType[type] = services;
        }

        /// <inheritdoc/>
        public IEnumerable<Service> GetFor(HostType type)
        {
            ThrowIfUnknownHostType(type);

            return _servicesPerHostType[type];
        }

        void ThrowIfUnknownHostType(HostType type)
        {
            if (!_servicesPerHostType.ContainsKey(type)) throw new UnknownHostType(type);
        }
    }
}