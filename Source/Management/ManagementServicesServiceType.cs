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
        /// <summary>
        /// Gets the identifying name for the <see cref="ManagementServicesServiceType"/>
        /// </summary>
        public const string Name = "Management";

        /// <inheritdoc/>
        public ServiceType Identifier => Name;

        /// <inheritdoc/>
        public Type BindingInterface => typeof(ICanBindManagementServices);

        /// <inheritdoc/>
        public EndpointType EndpointType => EndpointType.Public;
    }
}