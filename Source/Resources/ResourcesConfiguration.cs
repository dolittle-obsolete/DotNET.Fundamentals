/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourcesConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Dictionary<ResourceType, ResourceTypeConfiguration> ConfigurationByType { get; set; }
    }
}