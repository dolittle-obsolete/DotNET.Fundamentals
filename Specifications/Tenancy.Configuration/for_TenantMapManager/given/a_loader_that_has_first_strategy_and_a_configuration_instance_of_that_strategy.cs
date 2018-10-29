/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Machine.Specifications;
using Moq;
using Dolittle.Tenancy.Configuration.Specs.given;
using System;

namespace Dolittle.Tenancy.Configuration.Specs.for_TenantMapManager.given
{
    public class a_loader_that_has_first_strategy_and_a_configuration_instance_of_that_strategy : Specs.given.all
    {
        protected static Mock<ITenantStrategyLoader> tenant_strategy_loader;

        Establish context = () => 
        {
            tenant_strategy_loader = new Mock<ITenantStrategyLoader>();

            tenant_strategy_loader.Setup(_ => _.Strategy).Returns(first_strategy);
            tenant_strategy_loader.Setup(_ => _.GetStrategyInstance(Moq.It.IsAny<Type>())).Returns(new configuration_instance_of_first_strategy());
        };
    }
}