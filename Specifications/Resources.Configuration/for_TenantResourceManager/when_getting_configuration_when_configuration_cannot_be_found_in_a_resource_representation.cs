/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Machine.Specifications;
using Dolittle.Resources.Configuration.Specs.given;
using System;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager
{
    public class when_getting_configuration_when_configuration_cannot_be_found_in_a_resource_representation : given.an_instance_of_mongo_db_read_models_representation_and_a_system_providing_read_model_repository_configuration
    {
        static TenantResourceManager tenant_resource_manager;
        static Exception exception_result;
        Establish context = () => tenant_resource_manager = new TenantResourceManager(instance_of_mongo_db_read_models_representation_mock.Object, a_system_provding_resource_configuration_for_read_models_mock.Object);

        Because of_trying_to_get_an_event_store_configuration = () => exception_result = Catch.Exception(() => tenant_resource_manager.GetConfigurationFor<EventStoreConfiguration>(tenant_id));

        It should_throw_an_exception = () => exception_result.ShouldNotBeNull();
        It should_throw_NoResourceTypeMatchingConfigurationType = () => exception_result.ShouldBeOfExactType(typeof(NoResourceTypeMatchingConfigurationType));
    }
}