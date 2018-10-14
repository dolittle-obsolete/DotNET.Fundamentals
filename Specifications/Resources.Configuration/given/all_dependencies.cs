/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Dolittle.Tenancy;

namespace Dolittle.Resources.Configuration.Specs.given
{
    public class all_dependencies
    {
        protected static readonly TenantId tenant_id = TenantId.System; 
        protected static readonly ResourceType read_models_resource_type = "readModels";
        protected static readonly ResourceType event_store_resource_type = "eventStore";
        protected static readonly ResourceType another_resource_type = "anotherResourceType";
        protected static readonly ResourceTypeImplementation mongo_db_resource_type_implementation = "MongoDB";

        protected static readonly ResourceTypeImplementation azure_resource_type_implementation = "Azure";
    }
}