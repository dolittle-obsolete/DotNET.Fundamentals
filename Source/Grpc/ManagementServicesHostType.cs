/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Grpc
{
    /// <summary>
    /// Represents a <see cref="IRepresentHostType">host type</see> that is for management communication
    /// </summary>
    /// <remarks>
    /// Management is considered the channel where tooling is connecting for management
    /// </remarks>
    public class ManagementServicesHostType : IRepresentHostType
    {
        /// <summary>
        /// Gets the identifying name for the <see cref="ManagementServicesHostType"/>
        /// </summary>
        public const string Name = "Management";

        /// <summary>
        /// Initializes a new instance of <see cref="ManagementServicesHostType"/>
        /// </summary>
        /// <param name="configuration"><see cref="HostsConfiguration"/> containing the <see cref="HostConfiguration"/> for the host type</param>
        public ManagementServicesHostType(HostsConfiguration configuration)
        {
            Configuration = configuration.ContainsKey(Identifier)?configuration[Identifier]:HostsConfigurationDefaultProvider.ManagementHostTypeDefaultConfiguration;
        }

        /// <inheritdoc/>
        public HostType Identifier => Name;

        /// <inheritdoc/>
        public Type BindingInterface => typeof(ICanBindManagementServices);

        /// <inheritdoc/>
        public HostConfiguration Configuration { get; }
    }
}