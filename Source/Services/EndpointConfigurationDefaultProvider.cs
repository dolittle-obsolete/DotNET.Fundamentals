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
        /// Accesses the static configurations for providing default <see cref="EndpointConfiguration"/> for different <see cref="ServiceType">service types</see>
        /// </summary>
        public readonly static Dictionary<EndpointType, EndpointConfiguration> Configurations = new Dictionary<EndpointType, EndpointConfiguration>();       

        /// <inheritdoc/>
        public EndpointsConfiguration Provide()
        {
            Configurations[EndpointType.Public] = new EndpointConfiguration(50052);
            Configurations[EndpointType.Private] = new EndpointConfiguration(50053);
            return new EndpointsConfiguration(Configurations);
        }
    }
}