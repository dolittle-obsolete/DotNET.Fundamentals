/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Dolittle.Tenancy;

namespace Dolittle.ResourceTypes.Configuration.Specs.given
{
    public class all_dependencies
    {
        protected static readonly TenantId tenant_id = TenantId.System; 
        public static readonly ResourceType first_resource_type = "first_resource_type";
        public static readonly ResourceType second_resource_type = "second_resource_type";
        public static readonly ResourceType third_resource_type = "third_resource_type";
        public static readonly ResourceTypeImplementation first_resource_type_implementation = "first_resource_type_implementation";
        public static readonly ResourceTypeImplementation second_resource_type_implementation = "second_resource_type_implementation";
    }
}