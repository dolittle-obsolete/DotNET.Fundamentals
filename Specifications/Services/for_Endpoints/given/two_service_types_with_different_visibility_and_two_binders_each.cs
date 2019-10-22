/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Types;
using Dolittle.Types.Testing;
using Grpc.Core;
using Machine.Specifications;
using Moq;

namespace Dolittle.Services.for_Endpoints.given
{
    public class two_service_types_with_different_visibility_and_two_binders_each
    {
        public interface ICanBindFirstServiceType : ICanBindServices {}
        public interface ICanBindSecondServiceType : ICanBindServices {}

        public class FirstServiceTypeFirstBinder : ICanBindFirstServiceType
        {
            public IEnumerable<Service> ServicesToBind;
            public IEnumerable<Service> BindServices() => ServicesToBind;
        }

        public class FirstServiceTypeSecondBinder : ICanBindFirstServiceType
        {
            public IEnumerable<Service> ServicesToBind;
            public IEnumerable<Service> BindServices() => ServicesToBind;
        }

        public class SecondServiceTypeFirstBinder : ICanBindSecondServiceType
        {
            public IEnumerable<Service> ServicesToBind;
            public IEnumerable<Service> BindServices() => ServicesToBind;
        }

        public class SecondServiceTypeSecondBinder : ICanBindSecondServiceType
        {
            public IEnumerable<Service> ServicesToBind;
            public IEnumerable<Service> BindServices() => ServicesToBind;
        }




        protected const string first_service_type_identifier = "My First Service Type";
        protected const string second_service_type_identifier = "My Second Service Type";

        protected static Mock<IContainer> container;
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IBoundServices> bound_services;
        protected static ILogger logger;
        

        protected static Mock<IRepresentServiceType> first_service_type;
        protected static Mock<IRepresentServiceType> second_service_type;

        protected static StaticInstancesOf<IRepresentServiceType> service_types;

        protected static FirstServiceTypeFirstBinder first_binder_first_type;
        protected static FirstServiceTypeSecondBinder second_binder_first_type;
        protected static SecondServiceTypeFirstBinder first_binder_second_type;
        protected static SecondServiceTypeSecondBinder second_binder_second_type;
        protected static Mock<IEndpoint> endpoint;

        protected static ServiceType first_identifier;
        protected static ServiceType second_identifier;
        protected static Type first_binding_interface;
        protected static Type second_binding_interface;
        protected static EndpointConfiguration public_configuration;
        protected static EndpointConfiguration private_configuration;
        protected static EndpointsConfiguration endpoints_configuration;

        protected static Service first_service_type_first_binder_first_service;
        protected static Service first_service_type_first_binder_second_service;
        protected static Service first_service_type_second_binder_first_service;
        protected static Service first_service_type_second_binder_second_service;
        protected static Service second_service_type_first_binder_first_service;
        protected static Service second_service_type_first_binder_second_service;
        protected static Service second_service_type_second_binder_first_service;
        protected static Service second_service_type_second_binder_second_service;

        Establish context = () =>
        {
            container = new Mock<IContainer>();
            type_finder = new Mock<ITypeFinder>();
            bound_services = new Mock<IBoundServices>();
            logger = Mock.Of<ILogger>();

            endpoint = new Mock<IEndpoint>();
            container.Setup(_ => _.Get<IEndpoint>()).Returns(endpoint.Object);

            first_identifier = first_service_type_identifier;
            first_binding_interface = typeof(ICanBindFirstServiceType);
            second_binding_interface = typeof(ICanBindSecondServiceType);

            public_configuration = new EndpointConfiguration();
            private_configuration = new EndpointConfiguration();

            endpoints_configuration = new EndpointsConfiguration(new Dictionary<EndpointVisibility, EndpointConfiguration>
                {
                    { EndpointVisibility.Public, public_configuration },
                    { EndpointVisibility.Private, private_configuration }
                });

            first_binder_first_type = new FirstServiceTypeFirstBinder();
            second_binder_first_type = new FirstServiceTypeSecondBinder();

            first_binder_second_type = new SecondServiceTypeFirstBinder();
            second_binder_second_type = new SecondServiceTypeSecondBinder();

            var firstBinderFirstServiceType = typeof(FirstServiceTypeFirstBinder);
            var secondBinderFirstServiceType = typeof(FirstServiceTypeSecondBinder);

            var firstBinderSecondServiceType = typeof(SecondServiceTypeFirstBinder);
            var secondBinderSecondServiceType = typeof(SecondServiceTypeSecondBinder);
            
            type_finder.Setup(_ => _.FindMultiple(typeof(ICanBindFirstServiceType))).Returns(new [] { 
                firstBinderFirstServiceType,
                secondBinderFirstServiceType,
            });

            type_finder.Setup(_ => _.FindMultiple(typeof(ICanBindSecondServiceType))).Returns(new [] { 
                firstBinderSecondServiceType,
                secondBinderSecondServiceType,
            });

            container.Setup(_ => _.Get(firstBinderFirstServiceType)).Returns(first_binder_first_type);
            container.Setup(_ => _.Get(secondBinderFirstServiceType)).Returns(second_binder_first_type);

            container.Setup(_ => _.Get(firstBinderSecondServiceType)).Returns(first_binder_second_type);
            container.Setup(_ => _.Get(secondBinderSecondServiceType)).Returns(second_binder_second_type);

            first_service_type = new Mock<IRepresentServiceType>();
            first_service_type.SetupGet(_ => _.Identifier).Returns(first_identifier);
            first_service_type.SetupGet(_ => _.BindingInterface).Returns(first_binding_interface);
            first_service_type.SetupGet(_ => _.Visibility).Returns(EndpointVisibility.Public);

            second_service_type = new Mock<IRepresentServiceType>();
            second_service_type.SetupGet(_ => _.Identifier).Returns(second_identifier);
            second_service_type.SetupGet(_ => _.BindingInterface).Returns(second_binding_interface);
            second_service_type.SetupGet(_ => _.Visibility).Returns(EndpointVisibility.Private);

            service_types = new StaticInstancesOf<IRepresentServiceType>(first_service_type.Object, second_service_type.Object);

            first_service_type_first_binder_first_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            first_service_type_first_binder_second_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            first_service_type_second_binder_first_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            first_service_type_second_binder_second_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);

            second_service_type_first_binder_first_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            second_service_type_first_binder_second_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            second_service_type_second_binder_first_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);
            second_service_type_second_binder_second_service = new Service(null, ServerServiceDefinition.CreateBuilder().Build(), null);

            first_binder_first_type.ServicesToBind = new[] { 
                first_service_type_first_binder_first_service,
                first_service_type_first_binder_second_service
            };

            second_binder_first_type.ServicesToBind = new[] { 
                first_service_type_second_binder_first_service,
                first_service_type_second_binder_second_service
            };

            first_binder_second_type.ServicesToBind = new[] { 
                second_service_type_first_binder_first_service,
                second_service_type_first_binder_second_service
            };

            second_binder_second_type.ServicesToBind = new[] { 
                second_service_type_second_binder_first_service,
                second_service_type_second_binder_second_service
            };
        };
    }
}