/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Logging;
using grpc = Grpc.Core;
using Grpc.Core.Interceptors;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IEndpoint"/>
    /// </summary>
    public class Endpoint : IEndpoint
    {
        readonly ILogger _logger;
        grpc::Server _server;

        /// <summary>
        /// Initializes a new instance of <see cref="Endpoint"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Endpoint(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Destructs the <see cref="Endpoint"/> instance
        /// </summary>
        ~Endpoint()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _server?.ShutdownAsync().Wait();
        }

        /// <inheritdoc/>
        public void Start(EndpointVisibility type, EndpointConfiguration configuration, IEnumerable<Service> services)
        {
            try
            {
                var keepAliveTime = new grpc.ChannelOption("grpc.keepalive_time", 1000);
                var keepAliveTimeout = new grpc.ChannelOption("grpc.keepalive_timeout_ms", 500);
                var keepAliveWithoutCalls = new grpc.ChannelOption("grpc.keepalive_permit_without_calls", 1);
                _server = new grpc::Server(new[] {
                    keepAliveTime,
                    keepAliveTimeout,
                    keepAliveWithoutCalls
                })
                {
                    Ports = {
                    new grpc.ServerPort("0.0.0.0", configuration.Port, grpc::SslServerCredentials.Insecure)
                    }
                };

                _server
                    .Ports
                    .ForEach(_ =>
                        _logger.Information($"Starting {type} host on {_.Host}" + (_.Port > 0 ? $" for port {_.Port}" : string.Empty)));

                services.ForEach(_ => _server.Services.Add(_.ServerDefinition));

                _server.Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Couldn't start {type} host");
            }
        }
    }
}