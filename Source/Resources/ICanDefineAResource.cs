/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICanDefineAResource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        ResourceType    ResourceType { get; }

        /// <summary>
        /// 
        /// </summary>
        ResourceTypeName ResourceTypeName { get; }

        /// <summary>
        /// 
        /// </summary>
        Type ConfigurationObjectType { get; }
    }
}