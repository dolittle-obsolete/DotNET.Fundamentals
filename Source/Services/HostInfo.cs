/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Services
{
    /// <summary>
    /// Represents the host information for a <see cref="HostType"/>
    /// </summary>
    public class HostInfo
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HostInfo"/>
        /// </summary>
        /// <param name="hostType">The <see cref="HostType"/></param>
        /// <param name="configuration">The <see cref="HostConfiguration">configuration</see> for the host</param>
        public HostInfo(HostType hostType, HostConfiguration configuration)
        {
            HostType = hostType;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the identifier for the <see cref="HostType"/>
        /// </summary>
        public HostType HostType { get; }

        /// <summary>
        /// Gets the <see cref="HostConfiguration"/> for the <see cref="HostType"/>
        /// </summary>
        public HostConfiguration Configuration { get; }
    }
}