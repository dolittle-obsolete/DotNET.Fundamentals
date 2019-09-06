/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IEndpoints"/>
    /// </summary>
    [Singleton]
    public class Endpoints : IEndpoints
    {
        readonly IDictionary<EndpointType, List<IRepresentServiceType>> _serviceRepresentersForEndpointType = new Dictionary<EndpointType, List<IRepresentServiceType>>();
        readonly IDictionary<EndpointType, IEndpoint> _endpoints = new Dictionary<EndpointType, IEndpoint>();
        readonly IList<EndpointInfo> _endpointInfos = new List<EndpointInfo>();

        readonly EndpointsConfiguration _configuration;
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        readonly ILogger _logger;
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initializes a new instance of <see cref="Endpoints"/>
        /// </summary>
        /// <param name="serviceTypes">Instances of <see cref="IRepresentServiceType"/></param>
        /// <param name="configuration"><see cref="EndpointsConfiguration"/> for all endpoints</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding services to host</param>
        /// <param name="container"><see cref="IContainer"/> for working with instances of host binders</param>
        /// <param name="boundServices"><see cref="IBoundServices"/> for registering services that gets bound</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Endpoints(
            IInstancesOf<IRepresentServiceType> serviceTypes,
            EndpointsConfiguration configuration,
            ITypeFinder typeFinder,
            IContainer container,
            IBoundServices boundServices,
            ILogger logger)
        {
            _configuration = configuration;
            _serviceRepresentersForEndpointType = serviceTypes  .GroupBy(_ => _.EndpointType)
                                                                .ToDictionary(_ => _.Key, _ => _.ToList());

            _typeFinder = typeFinder;
            _container = container;
            _logger = logger;
            _boundServices = boundServices;
        }

        /// <summary>
        /// Destructs the <see cref="Endpoints"/> instance
        /// </summary>
        ~Endpoints()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach( (var type, var endpoint) in _endpoints ) endpoint.Dispose();
        }

        /// <inheritdoc/>
        public void Start()
        {
            _logger.Information("Starting all endpoints");

            var servicesByEndpointType = new Dictionary<EndpointType, List<Service>>();

            foreach ((var type, var serviceTypeRepresenters) in _serviceRepresentersForEndpointType)
            {
                var configuration = _configuration[type];
                if (configuration.Enabled)
                {
                    _logger.Information($"Preparing endpoint for {type} - running on port {configuration.Port}");
                    var endpoint = GetEndpointFor(type);

                    var services = new List<Service>();

                    serviceTypeRepresenters.ForEach(representer => 
                    {
                        var binders = _typeFinder.FindMultiple(representer.BindingInterface);
                        binders.ForEach(_ =>
                        {
                            var binder = _container.Get(_) as ICanBindServices;
                            _logger.Information($"Bind services from {_.AssemblyQualifiedName}");
                            services.AddRange(binder.BindServices());
                        });

                        _boundServices.Register(representer.Identifier, services);

                        if( !servicesByEndpointType.ContainsKey(type)) servicesByEndpointType[type] = new List<Service>();
                        servicesByEndpointType[type].AddRange(services);
                    });
                }
                else
                {
                    _logger.Information($"{type} endpoint is disabled");
                }
            }

            foreach( (var type, var endpoint) in _endpoints ) 
            {
                var configuration = _configuration[type];
                endpoint.Start(type, configuration, servicesByEndpointType[type]);
            }

        }

        /// <inheritdoc/>
        public IEnumerable<EndpointInfo> GetEndpoints()
        {
            return _endpointInfos;
        }

        IEndpoint GetEndpointFor(EndpointType type)
        {
            if( _endpoints.ContainsKey(type)) return _endpoints[type];
            var endpoint = _container.Get<IEndpoint>();
            _endpoints[type] = endpoint;
            return endpoint;
        }
    }
}