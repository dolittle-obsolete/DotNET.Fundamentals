/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class all_dependencies
    {
        protected static readonly TenantId tenant_id = TenantId.System; 
        protected static readonly ResourceType read_models_resource_type = "readModels";

        protected static readonly ResourceType event_store_resource_type = "eventStore";
    }
}