/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Configuration;

namespace Dolittle.Grpc
{
    /// <summary>
    /// Represents a <see cref="ICanProvideDefaultConfigurationFor{T}">default provider</see> for <see cref="HostsConfiguration"/>
    /// </summary>
    public class HostsConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<HostsConfiguration>
    {
        /// <inheritdoc/>
        public HostsConfiguration Provide()
        {
            return new HostsConfiguration(new Dictionary<HostType, HostConfiguration>());
        }
    }
}