// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Protobuf.for_Extensions
{
    public class when_adding_metadata_and_getting_it
    {
        static Execution.Contracts.ExecutionContext execution_context;
        static Execution.Contracts.ExecutionContext result;

        Establish context = () =>
        {
            execution_context = new Execution.Contracts.ExecutionContext
            {
                TenantId = Guid.NewGuid().ToProtobuf(),
                MicroserviceId = Guid.NewGuid().ToProtobuf(),
                CorrelationId = Guid.NewGuid().ToProtobuf(),
                Environment = Execution.Environment.Development,
                Version = Versioning.Version.NotSet.ToProtobuf()
            };

            execution_context.Claims.AddRange(new[]
            {
                new Security.Contracts.Claim { Key = "First", Value = "FirstValue", ValueType = "FirstValueType" },
                new Security.Contracts.Claim { Key = "Second", Value = "SecondValue", ValueType = "SecondValueType" }
            });
        };

        Because of = () =>
        {
            var entry = execution_context.ToArgumentsMetadata();
            var callContext = new CallContext();
            callContext.RequestHeaders.Add(entry);
            result = callContext.GetArgumentsMessage<Execution.Contracts.ExecutionContext>();
        };

        It should_be_equal = () => result.ShouldEqual(execution_context);
    }
}