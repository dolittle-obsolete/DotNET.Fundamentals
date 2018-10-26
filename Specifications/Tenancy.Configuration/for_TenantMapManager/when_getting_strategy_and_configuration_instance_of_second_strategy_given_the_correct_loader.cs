/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Dolittle.Tenancy.Configuration.Specs.given;
namespace Dolittle.Tenancy.Configuration.Specs.for_TenantMapManager
{
    public class when_getting_strategy_and_configuration_instance_of_second_strategy_given_the_correct_loader : given.a_loader_that_has_second_strategy_and_a_configuration_instance_of_that_strategy
    {
        static ITenantMapManager tenant_map_manager;
        static TenantStrategy result_strategy;
        static configuration_instance_of_second_strategy result_instance_of_strategy;

        Establish context = () => tenant_map_manager = new TenantMapManager(tenant_strategy_loader.Object); 

        Because of = () => 
        {
            result_strategy = tenant_map_manager.Strategy;
            result_instance_of_strategy = tenant_map_manager.InstanceOfStrategy<configuration_instance_of_second_strategy>();
        };

        It should_return_the_second_strategy = () => result_strategy.ShouldEqual(second_strategy);
        It should_return_the_configuration_instance_of_the_second_strategy = () => result_instance_of_strategy.ShouldNotBeNull();
    }
}