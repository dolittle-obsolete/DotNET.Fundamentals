// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Tenancy.Configuration.Specs.given;
using Machine.Specifications;

namespace Dolittle.Tenancy.Configuration.Specs.for_TenantMapManager
{
    public class when_getting_strategy_and_configuration_instance_of_a_strategy_given_a_loader_that_loads_another_strategy : given.a_loader_that_has_second_strategy_and_a_configuration_instance_of_that_strategy
    {
        static ITenantMapManager tenant_map_manager;
        static Exception result_exception;

        Establish context = () => tenant_map_manager = new TenantMapManager(tenant_strategy_loader.Object);

        Because of = () => result_exception = Catch.Exception(() => tenant_map_manager.InstanceOfStrategy<configuration_instance_of_first_strategy>());

        It should_throw_an_exception = () => result_exception.ShouldNotBeNull();
        It should_throw_wrong_strategy_configuration = () => result_exception.ShouldBeOfExactType(typeof(WrongStrategyConfiguration));
    }
}