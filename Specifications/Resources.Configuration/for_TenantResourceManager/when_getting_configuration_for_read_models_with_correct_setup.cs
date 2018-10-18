/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Dolittle.Resources.Configuration.Specs.given;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager
{
    public class when_getting_configuration_for_read_models_with_correct_setup : given.a_resource_type_with_one_service_and_a_system_providing_its_configuration
    {
        static TenantResourceManager tenant_resource_manager;
        static configuration_for_first_resource_type configuration;
        Establish context = () => tenant_resource_manager = new TenantResourceManager(instance_of_mongo_db_read_models_representation_mock.Object, a_system_provding_resource_configuration_for_read_models_mock.Object);

        Because of_trying_to_get_the_configuration_for_the_first_resource_type = () => configuration = tenant_resource_manager.GetConfigurationFor<configuration_for_first_resource_type>(tenant_id);

        It should_return_the_configuration = () => configuration.ShouldNotBeNull();
        
    }
}