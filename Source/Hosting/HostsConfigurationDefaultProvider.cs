/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Hosting
{
    /// <summary>
    /// Represents a <see cref="ICanProvideDefaultConfigurationFor{T}">default provider</see> for <see cref="HostsConfiguration"/>
    /// </summary>
    public class HostsConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<HostsConfiguration>
    {
        /// <summary>
        /// Accesses the static configurations for providing default <see cref="HostConfiguration"/> for different <see cref="HostType">host types</see>
        /// </summary>
        public readonly static Dictionary<HostType, HostConfiguration> Configurations = new Dictionary<HostType, HostConfiguration>();       

        /// <inheritdoc/>
        public HostsConfiguration Provide()
        {
            return new HostsConfiguration(Configurations);
        }
    }
}