// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System;
using System.Linq.Expressions;
using contracts::Dolittle.Services.Contracts;
using Dolittle.Protobuf;
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

            Expression<Func<MyResponse, ReverseCallResponseContext>> expression = _ => _.ResponseContext;

            reverse_call = new ReverseCall<MyResponse, MyRequest>(
                request,
                response_stream_writer.Object,
                call_id,
                expression);

            response_stream_writer.Setup(_ => _.WriteAsync(Moq.It.IsAny<MyResponse>())).Callback((MyResponse _) => response = _);
        };

        Because of = () => reverse_call.Reply(new MyResponse() { ResponseContext = new ReverseCallResponseContext() });

        It should_set_the_call_id = () => response.ResponseContext.CallId.To<ReverseCallId>().ShouldEqual(call_id);
    }
}