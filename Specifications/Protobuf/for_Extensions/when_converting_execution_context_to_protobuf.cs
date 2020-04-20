// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.Security;
using Dolittle.Tenancy;
using Machine.Specifications;

namespace Dolittle.Protobuf.for_Extensions
{
    public class when_converting_execution_context_to_protobuf
    {
        static ExecutionContext execution_context;
        static Execution.Contracts.ExecutionContext result;

        Establish context = () =>
        {
            execution_context = new ExecutionContext(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Versioning.Version.NotSet,
                Execution.Environment.Development,
                Guid.NewGuid(),
                new Claims(new[]
                {
                    new Claim("First", "FirstValue", "FirstType"),
                    new Claim("Second", "SecondValue", "SecondType")
                }),
                CultureInfo.InvariantCulture);
        };

        Because of = () => result = execution_context.ToProtobuf();

        It should_hold_the_correct_microservice = () => result.MicroserviceId.To<Microservice>().ShouldEqual(execution_context.Microservice);
        It should_hold_the_correct_tenant = () => result.TenantId.To<TenantId>().ShouldEqual(execution_context.Tenant);
        It should_hold_the_correct_version = () => result.Version.ToVersion().ShouldEqual(execution_context.Version);
        It should_hold_the_correct_correlation_id = () => result.CorrelationId.To<CorrelationId>().ShouldEqual(execution_context.CorrelationId);
        It should_hold_the_correct_claims = () => result.Claims.ToClaims().ShouldEqual(execution_context.Claims);
    }
}