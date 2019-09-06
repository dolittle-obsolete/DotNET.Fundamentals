/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Services
{
    /// <summary>
    /// Represents the configuration typically used by a <see cref="IEndpoint"/>
    /// </summary>
    public class EndpointConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EndpointConfiguration"/>
        /// </summary>
        public EndpointConfiguration() {}

        /// <summary>
        /// Initializes a new instance of <see cref="EndpointConfiguration"/>
        /// </summary>
        /// <param name="port">Port to run the host on</param>
        public EndpointConfiguration(int port) => Port = port;

        /// <summary>
        /// Gets or sets whether or not the interaction server is enabled
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// The port to use for exposing the <see cref="IEndpoint"/> on
        /// </summary>
        public int Port { get; set; } = 50051;
    }
}