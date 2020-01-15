// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.Tenancy;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject"/> for resources per tenant.
    /// </summary>
    [Name("resources")]
    public class ResourceConfigurationsByTenant :
        ReadOnlyDictionary<TenantId, ReadOnlyDictionary<ResourceType, dynamic>>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceConfigurationsByTenant"/> class.
        /// </summary>
        /// <param name="dictionary">The configuration to initialize with.</param>
        public ResourceConfigurationsByTenant(
            IDictionary<TenantId, ReadOnlyDictionary<ResourceType, dynamic>> dictionary)
            : base(dictionary)
        {
        }
    }
}