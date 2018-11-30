/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.Tenancy;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject"/> for resources per tenant
    /// </summary>
    [Name("resources")]
    public class ResourceConfigurationsByTenant : 
        ReadOnlyDictionary<TenantId, ReadOnlyDictionary<ResourceType, dynamic>>, 
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceConfigurationsByTenant"/>
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public ResourceConfigurationsByTenant(
            IDictionary<TenantId, ReadOnlyDictionary<ResourceType, dynamic>> dictionary) : base(dictionary)
        {
 
        }
    }
}