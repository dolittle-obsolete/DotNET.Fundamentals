// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dolittle.Services.Contracts;
using Machine.Specifications;

namespace Dolittle.Services.for_ReverseCallDispatcher.when_receiving_arguments
{
    public class and_registration_arguments_call_context_does_not_have_execution_context : given.a_dispatcher
    {
        static bool result;

        Establish context = () =>
        {
            client_stream.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            client_stream.SetupGet(_ => _.Current).Returns(new MyClientMessage { Arguments = new MyConnectArguments { Context = new ReverseCallArgumentsContext() } });
        };

        Because of = () => result = dispatcher.ReceiveArguments(CancellationToken.None).GetAwaiter().GetResult();

        It should_return_false = () => result.ShouldBeFalse();
        It should_not_set_arguments = () => dispatcher.Arguments.ShouldBeNull();

        It should_not_change_execution_context = () => execution_context_manager
            .Verify(
                _ => _.CurrentFor(
                    Moq.It.IsAny<Execution.ExecutionContext>(),
                    Moq.It.IsAny<string>(),
                    Moq.It.IsAny<int>(),
                    Moq.It.IsAny<string>()), Moq.Times.Never);
    }
}