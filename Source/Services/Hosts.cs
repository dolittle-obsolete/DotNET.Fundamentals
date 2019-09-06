/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IHosts"/>
    /// </summary>
    [Singleton]
    public class Hosts : IHosts
    {
        readonly IDictionary<Type, HostInfo> _bindersWithConfigAccessor = new Dictionary<Type, HostInfo>();

        readonly IList<IHost> _hosts = new List<IHost>();
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        readonly ILogger _logger;
        readonly IBoundServices _boundServices;

        /// <summary>
        /// Initializes a new instance of <see cref="Hosts"/>
        /// </summary>
        /// <param name="serviceTypes">Instances of <see cref="IRepresentServiceType"/></param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding services to host</param>
        /// <param name="container"><see cref="IContainer"/> for working with instances of host binders</param>
        /// <param name="boundServices"><see cref="IBoundServices"/> for registering services that gets bound</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Hosts(
            IInstancesOf<IRepresentServiceType> serviceTypes,
            ITypeFinder typeFinder,
            IContainer container,
            IBoundServices boundServices,
            ILogger logger)
        {
            serviceTypes.ForEach(_ =>
                _bindersWithConfigAccessor[_.BindingInterface] =
                new HostInfo(_.Identifier, _.Configuration)
            );

            _typeFinder = typeFinder;
            _container = container;
            _logger = logger;
            _boundServices = boundServices;
        }

        /// <summary>
        /// Destructs the <see cref="Hosts"/> instance
        /// </summary>
        ~Hosts()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _hosts.ForEach(_ => _.Dispose());
        }

        /// <inheritdoc/>
        public void Start()
        {
            _logger.Information("Starting all hosts");

            foreach ((var type, var hostInfo) in _bindersWithConfigAccessor)
            {
                if (hostInfo.Configuration.Enabled)
                {
                    _logger.Information($"Preparing host for {hostInfo.ServiceType}");

                    var services = new List<Service>();
                    var binders = _typeFinder.FindMultiple(type);

                    binders.ForEach(_ =>
                    {
                        var binder = _container.Get(_) as ICanBindServices;
                        _logger.Information($"Bind services from {_.AssemblyQualifiedName}");
                        services.AddRange(binder.BindServices());
                    });

                    var host = _container.Get<IHost>();
                    _boundServices.Register(hostInfo.ServiceType, services);
                    host.Start(hostInfo.ServiceType, hostInfo.Configuration, services);
                    _hosts.Add(host);
                }
                else
                {
                    _logger.Information($"{hostInfo.ServiceType} host is disabled");
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<HostInfo> GetHosts()
        {
            return _bindersWithConfigAccessor.Values;
        }
    }
}