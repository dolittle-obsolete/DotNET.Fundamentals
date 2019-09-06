/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Types;
using Dolittle.Types.Testing;
using Machine.Specifications;
using Moq;

namespace Dolittle.Services.given
{
    public class one_host_type_with_two_binders
    {
        protected const string host_type_identifier = "My Host Type";

        protected static Mock<IContainer> container;
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IBoundServices> bound_services;
        protected static ILogger logger;
        

        protected static Mock<IRepresentHostType> host_type;

        protected static StaticInstancesOf<IRepresentHostType> host_types;

        protected static Mock<ICanBindMyHostType> first_binder;
        protected static Mock<ICanBindMyHostType> second_binder;
        protected static Mock<IHost> host;

        protected static HostType identifier;
        protected static Type binding_interface;
        protected static HostConfiguration configuration;

        Establish context = () =>
        {
            container = new Mock<IContainer>();
            type_finder = new Mock<ITypeFinder>();
            bound_services = new Mock<IBoundServices>();
            logger = Mock.Of<ILogger>();

            host = new Mock<IHost>();
            container.Setup(_ => _.Get<IHost>()).Returns(host.Object);

            identifier = host_type_identifier;
            binding_interface = typeof(ICanBindMyHostType);
            configuration = new HostConfiguration();

            first_binder = new Mock<ICanBindMyHostType>();
            second_binder = new Mock<ICanBindMyHostType>();
            var firstBinderType = first_binder.Object.GetType();
            var secondBinderType = second_binder.Object.GetType();
            
            type_finder.Setup(_ => _.FindMultiple(typeof(ICanBindMyHostType))).Returns(new [] { 
                firstBinderType,
                secondBinderType,
            });
            container.Setup(_ => _.Get(firstBinderType)).Returns(first_binder.Object);
            container.Setup(_ => _.Get(secondBinderType)).Returns(second_binder.Object);

            host_type = new Mock<IRepresentHostType>();
            host_type.SetupGet(_ => _.Identifier).Returns(identifier);
            host_type.SetupGet(_ => _.BindingInterface).Returns(binding_interface);
            host_type.SetupGet(_ => _.Configuration).Returns(configuration);
            host_types = new StaticInstancesOf<IRepresentHostType>(host_type.Object);
        };
    }
}