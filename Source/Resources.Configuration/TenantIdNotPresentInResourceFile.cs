/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration
{
    /// <summary>
    /// The exception that gets thrown when resources for a <see cref="TenantId"/> is not found in the resource file.
    /// </summary>
    public class TenantIdNotPresentInResourceFile : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="TenantIdNotPresentInResourceFile"/>
        /// </summary>
        /// <param name="tenantId">The id that's not found in the resource file</param>
        public TenantIdNotPresentInResourceFile(TenantId tenantId)
            : base($"Expected resources under a tenant with {typeof(TenantId).FullName}: '{tenantId.Value.ToString()}', but the {typeof(TenantId).FullName} was not found in the resource file.")
        { }
    }
}