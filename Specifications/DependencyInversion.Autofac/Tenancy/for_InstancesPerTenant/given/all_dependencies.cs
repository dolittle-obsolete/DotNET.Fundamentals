/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;

namespace Dolittle.DependencyInversion.Autofac.Tenancy.for_InstancesPerTenant.given
{
    public class all_dependencies
    {
        protected static Mock<ITenantKeyCreator> tenant_key_creator;
        protected static Mock<ITypeActivator> type_activator;

        Establish context = () =>
        {
            tenant_key_creator = new Mock<ITenantKeyCreator>();
            type_activator = new Mock<ITypeActivator>();
        };
    }
}