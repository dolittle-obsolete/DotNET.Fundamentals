/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Tenancy;

namespace Dolittle.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceTypeConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ResourceTypeName Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Dictionary<TenantId, object>   Configurations { get; set; }
    }
}