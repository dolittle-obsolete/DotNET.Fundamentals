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
    /// Represents the configuration for hosts by <see cref="ServiceType"/>
    /// </summary>
    [Name("endpoints")]
    public class EndpointsConfiguration : 
        ReadOnlyDictionary<EndpointType, EndpointConfiguration>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EndpointsConfiguration"/>
        /// </summary>
        /// <param name="configuration">Dictionary for <see cref="ServiceType"/> with <see cref="EndpointConfiguration"/></param>
        public EndpointsConfiguration(IDictionary<EndpointType, EndpointConfiguration> configuration) : base(configuration) {}
    }
}