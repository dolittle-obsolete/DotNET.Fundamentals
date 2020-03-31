// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IContainer = Dolittle.DependencyInversion.IContainer;

namespace Dolittle.Hosting.Microsoft
{
    /// <summary>
    /// Represents an implementation of <see cref="IServiceProviderFactory{T}"/> that builds upon the <see cref="AutofacServiceProviderFactory"/> and adds services from the Dolittle boot process.
    /// </summary>
    public class ServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        readonly AutofacServiceProviderFactory _autofacFactory;
        readonly HostBuilderContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProviderFactory"/> class.
        /// </summary>
        /// <param name="context">The <see cref="HostBuilderContext"/>.</param>
        public ServiceProviderFactory(HostBuilderContext context)
        {
            _autofacFactory = new AutofacServiceProviderFactory();
            _context = context;
        }

        /// <inheritdoc/>
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = _autofacFactory.CreateBuilder(services);

            var bootResult = Bootloader.Configure(_ =>
            {
                if (_context.HostingEnvironment.IsDevelopment()) _.Development();
                _.SkipBootprocedures();
                _.UseContainer<ServiceProviderContainer>();
            }).Start();

            builder.AddDolittle(bootResult.Assemblies, bootResult.Bindings);

            return builder;
        }

        /// <inheritdoc/>
        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            var serviceProvider = _autofacFactory.CreateServiceProvider(containerBuilder);

            var container = serviceProvider.GetService(typeof(IContainer)) as IContainer;
            DependencyInversion.Booting.Boot.ContainerReady(container);
            BootStages.ContainerReady(container);

            var bootProcedures = container.Get<IBootProcedures>();
            bootProcedures.Perform();

            return serviceProvider;
        }
    }
}
