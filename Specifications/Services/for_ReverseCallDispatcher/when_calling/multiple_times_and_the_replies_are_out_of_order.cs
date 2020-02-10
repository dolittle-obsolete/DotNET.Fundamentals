// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Logging;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

#pragma warning disable CA2008

namespace Dolittle.Services.for_ReverseCallDispatcher.when_calling
{
    public class multiple_times_and_the_replies_are_out_of_order
    {
        const int total_requests = 5;
        static ReverseCallDispatcher<MyResponse, MyRequest> dispatcher;
        static Mock<IAsyncStreamReader<MyResponse>> response_stream;
        static Mock<IServerStreamWriter<MyRequest>> request_stream;
        static CallContext call_context;
        static List<MyResponse> responses;
        static List<ulong> expected_ordering;

        Establish context = () =>
        {
            response_stream = new Mock<IAsyncStreamReader<MyResponse>>();
            request_stream = new Mock<IServerStreamWriter<MyRequest>>();
            call_context = new CallContext();

            responses = new List<MyResponse>();
            expected_ordering = new List<ulong>();

            MyResponse current = null;

            var manualResetEvent = new ManualResetEventSlim(false);
            var queue = new Queue<MyResponse>();

            response_stream.SetupGet(_ => _.Current).Returns(() => current);
            response_stream.Setup(_ => _.MoveNext(Moq.It.IsAny<CancellationToken>())).Returns((CancellationToken _) =>
            {
                manualResetEvent.Wait();

                if (queue.Count == 0)
                {
                    return Task.FromResult(false);
                }

                current = queue.Dequeue();

                return Task.FromResult(true);
            });

            var numbers = Enumerable.Range(1, total_requests).OrderBy(_ => Guid.NewGuid()).ToArray();

            request_stream.Setup(_ => _.WriteAsync(Moq.It.IsAny<MyRequest>())).Callback((MyRequest _) =>
            {
                var callNumber = (ulong)numbers[_.CallNumber - 1];
                queue.Enqueue(new MyResponse { CallNumber = callNumber });
                if (_.CallNumber == total_requests)
                {
                    manualResetEvent.Set();
                }
            });

            dispatcher = new ReverseCallDispatcher<MyResponse, MyRequest>(
                response_stream.Object,
                request_stream.Object,
                call_context,
                _ => _.CallNumber,
                _ => _.CallNumber,
                Mock.Of<ILogger>());
        };

        Because of = () =>
        {
            var tasks = new List<Task>();
            var tcs = new TaskCompletionSource<bool>();
            var responsesReceived = 0;

            for (var i = 0; i < total_requests; i++)
            {
                dispatcher.Call(new MyRequest(), _ =>
                {
                    responsesReceived++;
                    responses.Add(_);

                    if (responsesReceived == total_requests)
                    {
                        tcs.SetResult(true);
                    }
                });

                expected_ordering.Add((ulong)i + 1);
            }

            tcs.Task.Wait();
        };

        It should_respond_in_same_order = () => responses.Select(_ => _.CallNumber).ToArray().ShouldEqual(expected_ordering.ToArray());
    }
}