/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Autofac;
using Autofac.Core;

namespace Dolittle.DependencyInversion.Autofac.Tenancy
{
    /// <summary>
    /// Extensions for <see cref="ContainerBuilder"/> related to <see cref="BindingsPerTenantsRegistrationSource"/>
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Add <see cref="BindingsPerTenantsRegistrationSource"/> as a <see cref="IRegistrationSource"/>
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void AddBindingsPerTenantRegistrationSource(this ContainerBuilder containerBuilder)
        {
            var tenantKeyCreator = new TenantKeyCreator(containerBuilder);
            var typeActivator = new TypeActivator(containerBuilder);
            var instancesPerTenant = new InstancesPerTenant(tenantKeyCreator, typeActivator);
            containerBuilder.RegisterSource(new BindingsPerTenantsRegistrationSource(instancesPerTenant));
        }
    }
}