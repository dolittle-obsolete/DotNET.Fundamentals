// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a <see cref="ICanProvideDefaultConfigurationFor{T}">default provider</see> for <see cref="EndpointsConfiguration"/>.
    /// </summary>
    public class EndpointConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<EndpointsConfiguration>
    {
        /// <summary>
        /// The default public port.
        /// </summary>
        public const int DefaultPublicPort = 50052;

        /// <summary>
        /// The default private port.
        /// </summary>
        public const int DefaultPrivatePort = 50053;

        /// <summary>
        /// Accesses the static configurations for providing default <see cref="EndpointConfiguration"/> for different <see cref="ServiceType">service types</see>.
        /// </summary>
        public static readonly Dictionary<EndpointVisibility, EndpointConfiguration> Configurations = new Dictionary<EndpointVisibility, EndpointConfiguration>();

        /// <inheritdoc/>
        public EndpointsConfiguration Provide()
        {
            Configurations[EndpointVisibility.Public] = new EndpointConfiguration(DefaultPublicPort);
            Configurations[EndpointVisibility.Private] = new EndpointConfiguration(DefaultPrivatePort);
            return new EndpointsConfiguration(Configurations);
        }
    }
}