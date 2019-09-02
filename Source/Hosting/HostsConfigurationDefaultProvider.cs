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
        internal static HostConfiguration ManagementHostTypeDefaultConfiguration = new HostConfiguration(50052);

        /// <inheritdoc/>
        public HostsConfiguration Provide()
        {
            var configurations = new Dictionary<HostType, HostConfiguration>
            {
                [ManagementServicesHostType.Name] = ManagementHostTypeDefaultConfiguration
            };
            return new HostsConfiguration(configurations);
        }
    }
}