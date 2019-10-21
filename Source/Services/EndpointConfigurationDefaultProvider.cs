/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a <see cref="ICanProvideDefaultConfigurationFor{T}">default provider</see> for <see cref="EndpointsConfiguration"/>
    /// </summary>
    public class EndpointConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<EndpointsConfiguration>
    {
        /// <summary>
        /// Gets or sets the default public port
        /// </summary>
        public static int DefaultPublicPort = 50052;

        /// <summary>
        /// Gets or sets the default private port
        /// </summary>
        public static int DefaultPrivatePort = 50053;


        /// <summary>
        /// Accesses the static configurations for providing default <see cref="EndpointConfiguration"/> for different <see cref="ServiceType">service types</see>
        /// </summary>
        public readonly static Dictionary<EndpointVisibility, EndpointConfiguration> Configurations = new Dictionary<EndpointVisibility, EndpointConfiguration>();       

        /// <inheritdoc/>
        public EndpointsConfiguration Provide()
        {
            Configurations[EndpointVisibility.Public] = new EndpointConfiguration(DefaultPublicPort);
            Configurations[EndpointVisibility.Private] = new EndpointConfiguration(DefaultPrivatePort);
            return new EndpointsConfiguration(Configurations);
        }
    }
}