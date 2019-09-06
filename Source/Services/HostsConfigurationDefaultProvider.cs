/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents a <see cref="ICanProvideDefaultConfigurationFor{T}">default provider</see> for <see cref="HostsConfiguration"/>
    /// </summary>
    public class HostsConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<HostsConfiguration>
    {
        /// <summary>
        /// Accesses the static configurations for providing default <see cref="HostConfiguration"/> for different <see cref="ServiceType">service types</see>
        /// </summary>
        public readonly static Dictionary<ServiceType, HostConfiguration> Configurations = new Dictionary<ServiceType, HostConfiguration>();       

        /// <inheritdoc/>
        public HostsConfiguration Provide()
        {
            return new HostsConfiguration(Configurations);
        }
    }
}