/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Services;

namespace Dolittle.Management
{
    /// <summary>
    /// Represents a <see cref="IRepresentServiceType">service type</see> that is for management communication
    /// </summary>
    /// <remarks>
    /// Management is considered the channel where tooling is connecting for management
    /// </remarks>
    public class ManagementServicesServiceType : IRepresentServiceType
    {
        static HostConfiguration _defaultHostConfiguration;

        static ManagementServicesServiceType()
        {
            _defaultHostConfiguration = new HostConfiguration(50052);
            HostsConfigurationDefaultProvider.Configurations[ManagementServicesServiceType.Name] = _defaultHostConfiguration;
        }


        /// <summary>
        /// Gets the identifying name for the <see cref="ManagementServicesServiceType"/>
        /// </summary>
        public const string Name = "Management";

        /// <summary>
        /// Initializes a new instance of <see cref="ManagementServicesServiceType"/>
        /// </summary>
        /// <param name="configuration"><see cref="HostsConfiguration"/> containing the <see cref="HostConfiguration"/> for the service type</param>
        public ManagementServicesServiceType(HostsConfiguration configuration)
        {
            Configuration = configuration.ContainsKey(Identifier)?configuration[Identifier]:_defaultHostConfiguration;
        }

        /// <inheritdoc/>
        public ServiceType Identifier => Name;

        /// <inheritdoc/>
        public Type BindingInterface => typeof(ICanBindManagementServices);

        /// <inheritdoc/>
        public HostConfiguration Configuration { get; }
    }
}