// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA2008

namespace Dolittle.Services.Clients.for_ReverseCallClientManager
{
    public class when_handling_request_from_server
    {
        static ReverseCallClientManager manager;
        static Mock<IClientStreamWriter<MyResponse>> response_stream_writer;
        static Mock<IAsyncStreamReader<MyRequest>> request_stream_reader;
        static TaskCompletionSource<ReverseCall<MyResponse, MyRequest>> result_source;
        static ReverseCall<MyResponse, MyRequest> result;
        static ManualResetEventSlim request_event;
        static MyRequest request;

        Establish context = () =>
        {
            manager = new ReverseCallClientManager();

            response_stream_writer = new Mock<IClientStreamWriter<MyResponse>>();
            request_stream_reader = new Mock<IAsyncStreamReader<MyRequest>>();

            var streamingCall = new AsyncDuplexStreamingCall<MyResponse, MyRequest>(
                response_stream_writer.Object,
                request_stream_reader.Object,
                Task.FromResult(new Metadata()),
                () => Grpc.Core.Status.DefaultSuccess,
                () => new Metadata(),
                () => { });

            request_event = new ManualResetEventSlim();
            request_stream_reader.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns((CancellationToken _) =>
            {
                request_event.Wait();
                request_event.Reset();

                return Task.FromResult(true);
            });

            request = new MyRequest { CallNumber = 42 };
            request_stream_reader.SetupGet(_ => _.Current).Returns(request);

            result_source = new TaskCompletionSource<ReverseCall<MyResponse, MyRequest>>();

            manager.Handle(
                streamingCall,
                _ => _.CallNumber,
                _ => _.CallNumber,
                reverseCall =>
                {
                    result_source.SetResult(reverseCall);
                    return Task.CompletedTask;
                },
                CancellationToken.None);
        };

        Because of = () =>
        {
            request_event.Set();
            result_source.Task.ContinueWith(_ => result = _.Result).Wait();
        };

        It should_set_the_call_number = () => result.CallNumber.ShouldEqual(request.CallNumber);
    }
}