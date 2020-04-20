// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Threading;
using System.Threading.Tasks;
using contracts::Dolittle.Services.Contracts;
using Dolittle.Logging;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA2008

namespace Dolittle.Services.for_ReverseCallDispatcher.when_calling
{
    public class and_there_as_an_immediate_reply
    {
        static ReverseCallDispatcher<MyResponse, MyRequest> dispatcher;
        static Mock<IAsyncStreamReader<MyResponse>> response_stream;
        static Mock<IServerStreamWriter<MyRequest>> request_stream;
        static CallContext call_context;

        static MyResponse response;
        static MyRequest request;
        static MyRequest request_sent;
        static MyResponse result;

        Establish context = () =>
        {
            response_stream = new Mock<IAsyncStreamReader<MyResponse>>();
            request_stream = new Mock<IServerStreamWriter<MyRequest>>();
            call_context = new CallContext();

            response = new MyResponse { ResponseContext = new ReverseCallResponseContext() };

            var manualResetEvent = new ManualResetEventSlim(false);

            response_stream.SetupGet(_ => _.Current).Returns(() => response);
            response_stream.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns((CancellationToken _) =>
                {
                    manualResetEvent.Wait();
                    manualResetEvent.Reset();

                    return Task.FromResult(true);
                });

            request = new MyRequest { RequestContext = new ReverseCallRequestContext() };
            request_stream.Setup(_ => _.WriteAsync(Moq.It.IsAny<MyRequest>())).Callback((MyRequest _) =>
            {
                request_sent = _;
                response.ResponseContext.CallId = _.RequestContext.CallId;
                manualResetEvent.Set();
            });

            dispatcher = new ReverseCallDispatcher<MyResponse, MyRequest>(
                response_stream.Object,
                request_stream.Object,
                call_context,
                _ => _.ResponseContext,
                _ => _.RequestContext,
                Mock.Of<ILogger>());
        };

        Because of = () => result = dispatcher.Call(request).GetAwaiter().GetResult();

        It should_send_the_request = () => request_sent.ShouldEqual(request);
        It should_return_the_expected_result = () => result.ShouldEqual(response);
    }
}