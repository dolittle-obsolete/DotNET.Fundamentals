// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Security;
using Machine.Specifications;

namespace Dolittle.Services.Clients.for_ReverseCallClient.when_connecting
{
    public class and_server_stream_responds_with_unexpected_response : given.a_reverse_call_client
    {
        static bool result;
        static Execution.ExecutionContext execution_context;

        Establish context = () =>
        {
            execution_context = new Execution.ExecutionContext(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Versioning.Version.NotSet,
                "",
                Guid.NewGuid(),
                Claims.Empty,
                CultureInfo.InvariantCulture);
            execution_context_manager.SetupGet(_ => _.Current).Returns(execution_context);
            server_stream.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
            server_stream.SetupGet(_ => _.Current).Returns(new MyServerMessage { Request = new MyRequest() });
        };

        Because of = () => result = reverse_call_client.Connect(new MyConnectArguments(), CancellationToken.None).GetAwaiter().GetResult();

        It should_return_false = () => result.ShouldBeFalse();
        It should_write_to_server_once = () => client_stream.Verify(_ => _.WriteAsync(Moq.It.IsAny<MyClientMessage>()), Moq.Times.Once);
        It should_read_from_server_once = () => server_stream.Verify(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>()), Moq.Times.Once);
        It should_not_set_connect_response = () => reverse_call_client.ConnectResponse.ShouldBeNull();
    }
}