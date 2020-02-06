// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Protobuf.for_Extensions
{
    public class when_getting_arguments_from_context_without_arguments
    {
        static CallContext call_context;
        static Exception result;

        Establish context = () => call_context = new CallContext();

        Because of = () => result = Catch.Exception(() => call_context.GetArgumentsMessage<Execution.Contracts.ExecutionContext>());

        It should_throw_missing_arguments_header_on_call_context = () => result.ShouldBeOfExactType<MissingArgumentsHeaderOnCallContext>();
    }
}