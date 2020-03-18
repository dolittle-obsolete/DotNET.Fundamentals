// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.Security;
using Dolittle.Tenancy;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Services.Clients.for_ExecutionContextClientMetadataProvider
{
    public class when_providing_metadata
    {
        static ExecutionContextClientMetadataProvider provider;
        static Mock<IExecutionContextManager> execution_context_manager;

        static ExecutionContext current;

        static IEnumerable<Metadata.Entry> result;
        static Execution.Contracts.ExecutionContext resulting_execution_context;

        Establish context = () =>
        {
            execution_context_manager = new Mock<IExecutionContextManager>();

            var claims = new Claims(new[]
            {
                new Claim("first", "firstValue", "firstValueType"),
                new Claim("second", "secondValue", "secondValueType")
            });

            current = new ExecutionContext(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Execution.Environment.Development,
                Guid.NewGuid(),
                claims,
                CultureInfo.InvariantCulture);

            execution_context_manager.SetupGet(_ => _.Current).Returns(current);

            provider = new ExecutionContextClientMetadataProvider(execution_context_manager.Object, Mock.Of<ILogger>());
        };

        Because of = () =>
        {
            result = provider.Provide();
            resulting_execution_context = Execution.Contracts.ExecutionContext.Parser.ParseFrom(result.First().ValueBytes);
        };

        It should_have_one_entry = () => result.Count().ShouldEqual(1);
        It should_hold_correct_application_id = () => resulting_execution_context.Application.To<Application>().ShouldEqual(current.Application);
        It should_hold_correct_microservice_id = () => resulting_execution_context.Microservice.To<Microservice>().ShouldEqual(current.Microservice);
        It should_hold_correct_tenant_id = () => resulting_execution_context.Tenant.To<TenantId>().ShouldEqual(current.Tenant);
        It should_hold_correct_correlation_id = () => resulting_execution_context.CorrelationId.To<CorrelationId>().ShouldEqual(current.CorrelationId);
        It should_hold_correct_claims = () => resulting_execution_context.Claims.Select(_ => new Claim(_.Key, _.Value, _.ValueType)).ShouldContainOnly(current.Claims);
    }
}