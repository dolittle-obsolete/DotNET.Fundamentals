/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Google.Protobuf.Reflection;
using Grpc.Core;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a gRPC service
    /// </summary>
    public class Service 
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Service"/>
        /// </summary>
        /// <param name="serverDefinition"><see cref="ServerServiceDefinition"/> that defines the service</param>
        /// <param name="descriptor"><see cref="ServiceDescriptor"/> that describes the service</param>
        public Service(ServerServiceDefinition serverDefinition, ServiceDescriptor descriptor)
        {
            ServerDefinition = serverDefinition;
            Descriptor = descriptor;
        }

        /// <summary>
        /// Get the <see cref="ServerServiceDefinition"/> for the service
        /// </summary>
        public ServerServiceDefinition ServerDefinition { get; }

        /// <summary>
        /// Get the <see cref="ServiceDescriptor"/> for the service
        /// </summary>
        public ServiceDescriptor Descriptor { get; } 
    }
}