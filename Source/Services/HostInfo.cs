/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Services
{
    /// <summary>
    /// Represents the host information for a <see cref="ServiceType"/>
    /// </summary>
    public class HostInfo
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HostInfo"/>
        /// </summary>
        /// <param name="serviceType">The <see cref="ServiceType"/></param>
        /// <param name="configuration">The <see cref="HostConfiguration">configuration</see> for the host</param>
        public HostInfo(ServiceType serviceType, HostConfiguration configuration)
        {
            ServiceType = serviceType;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the identifier for the <see cref="ServiceType"/>
        /// </summary>
        public ServiceType ServiceType { get; }

        /// <summary>
        /// Gets the <see cref="HostConfiguration"/> for the <see cref="ServiceType"/>
        /// </summary>
        public HostConfiguration Configuration { get; }
    }
}