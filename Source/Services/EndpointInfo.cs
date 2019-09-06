/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Services
{
    /// <summary>
    /// Represents the information about a specific <see cref="Endpoint"/>
    /// </summary>
    public class EndpointInfo 
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EndpointInfo"/>
        /// </summary>
        /// <param name="type"><see cref="EndpointType">Type</see> of endpoint</param>
        /// <param name="configuration"><see cref="EndpointConfiguration">Configuration</see> for the endoint</param>
        public EndpointInfo(EndpointType type, EndpointConfiguration configuration)
        {
            Type = type;
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public EndpointType Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public EndpointConfiguration Configuration { get; }
    }
}