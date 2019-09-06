/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;

namespace Dolittle.Services
{
    /// <summary>
    /// Represents the configuration for hosts by <see cref="HostType"/>
    /// </summary>
    [Name("hosts")]
    public class HostsConfiguration : 
        ReadOnlyDictionary<HostType, HostConfiguration>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HostsConfiguration"/>
        /// </summary>
        /// <param name="configuration">Dictionary for <see cref="HostType"/> with <see cref="HostConfiguration"/></param>
        public HostsConfiguration(IDictionary<HostType, HostConfiguration> configuration) : base(configuration) {}
    }
}