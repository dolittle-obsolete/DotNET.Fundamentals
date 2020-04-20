// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Protobuf;
using Dolittle.Services.Contracts;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Services.Clients.for_ReverseCall
{
    public class when_replying
    {
        static readonly ReverseCallId call_id = Guid.NewGuid();
        static ReverseCall<MyResponse, MyRequest> reverse_call;
        static MyRequest request;
        static Mock<IClientStreamWriter<MyResponse>> response_stream_writer;
        static MyResponse response;

        Establish context = () =>
        {
            request = new MyRequest();
            response_stream_writer = new Mock<IClientStreamWriter<MyResponse>>();

            Func<MyResponse, ReverseCallResponseContext> get_response_context = _ => _.ResponseContext;
            Action<MyResponse, ReverseCallResponseContext> set_response_context = (response, context) => response.ResponseContext = context;

            reverse_call = new ReverseCall<MyResponse, MyRequest>(
                request,
                response_stream_writer.Object,
                call_id,
                get_response_context,
                set_response_context);

            response_stream_writer.Setup(_ => _.WriteAsync(Moq.It.IsAny<MyResponse>())).Callback((MyResponse _) => response = _);
        };

        Because of = () => reverse_call.Reply(new MyResponse() { ResponseContext = new Contracts.ReverseCallResponseContext() });

        It should_set_the_call_id = () => response.ResponseContext.CallId.To<ReverseCallId>().ShouldEqual(call_id);
    }
}