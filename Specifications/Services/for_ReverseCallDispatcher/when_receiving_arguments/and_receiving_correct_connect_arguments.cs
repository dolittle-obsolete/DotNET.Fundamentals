// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Protobuf;
using Dolittle.Security;
using Dolittle.Services.Contracts;
using Machine.Specifications;

namespace Dolittle.Services.for_ReverseCallDispatcher.when_receiving_arguments
{
    public class and_receiving_correct_connect_arguments : given.a_dispatcher
    {
        static bool result;
        static Execution.Contracts.ExecutionContext execution_context;
        static MyConnectArguments arguments;

        Establish context = () =>
        {
            execution_context = new Execution.Contracts.ExecutionContext
                {
                    CorrelationId = Guid.NewGuid().ToProtobuf(),
                    Environment = "some env",
                    MicroserviceId = Guid.NewGuid().ToProtobuf(),
                    TenantId = Guid.NewGuid().ToProtobuf(),
                    Version = Versioning.Version.NotSet.ToProtobuf()
                };
            execution_context.Claims.Add(Claims.Empty.ToProtobuf());
            arguments = new MyConnectArguments { Context = new ReverseCallArgumentsContext { ExecutionContext = execution_context } };

            client_stream.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            client_stream.SetupGet(_ => _.Current).Returns(new MyClientMessage { Arguments = arguments });
        };

        Because of = () => result = dispatcher.ReceiveArguments(CancellationToken.None).GetAwaiter().GetResult();

        It should_return_true = () => result.ShouldBeTrue();
        It should_have_the_correct_arguments = () => dispatcher.Arguments.ShouldEqual(arguments);

        It should_change_execution_context = () => execution_context_manager
            .Verify(
                _ => _.CurrentFor(
                    execution_context.ToExecutionContext(),
                    Moq.It.IsAny<string>(),
                    Moq.It.IsAny<int>(),
                    Moq.It.IsAny<string>()), Moq.Times.Once);
    }
}