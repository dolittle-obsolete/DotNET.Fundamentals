/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Concurrent;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Grpc.Core;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundServices"/>
    /// </summary>
    [Singleton]
    public class BoundServices : IBoundServices
    {
        readonly ConcurrentDictionary<ServiceType, List<Service>>    _servicesPerServiceType = new  ConcurrentDictionary<ServiceType, List<Service>>();
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="BoundServices"/>
        /// </summary>
        public BoundServices(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Register(ServiceType type, IEnumerable<Service> services)
        {
            services.ForEach(service => 
            {
                _logger.Information($"Adding service '{service.Descriptor?.Name ?? "unknown"}'");
            });

            if( !_servicesPerServiceType.ContainsKey(type) ) _servicesPerServiceType[type] = new List<Service>();
            _servicesPerServiceType[type].AddRange(services);
        }

        /// <inheritdoc/>
        public bool HasFor(ServiceType type)
        {
            return _servicesPerServiceType.ContainsKey(type);
        }

        /// <inheritdoc/>
        public IEnumerable<Service> GetFor(ServiceType type)
        {
            ThrowIfUnknownServiceType(type);

            return _servicesPerServiceType[type];
        }

        void ThrowIfUnknownServiceType(ServiceType type)
        {
            if (!_servicesPerServiceType.ContainsKey(type)) throw new UnknownServiceType(type);
        }
    }
}