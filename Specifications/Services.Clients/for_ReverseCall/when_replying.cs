// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;
using Grpc.Core;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Services.Clients.for_ReverseCall
{
    public class when_replying
    {
        const ulong call_number = 42;
        static ReverseCall<MyResponse, MyRequest> reverse_call;
        static MyRequest request;
        static Mock<IClientStreamWriter<MyResponse>> response_stream_writer;
        static MyResponse response;

        Establish context = () =>
        {
            request = new MyRequest();
            response_stream_writer = new Mock<IClientStreamWriter<MyResponse>>();

            Expression<Func<MyResponse, ulong>> expression = _ => _.CallNumber;
            var propertyInfo = expression.GetPropertyInfo();

            reverse_call = new ReverseCall<MyResponse, MyRequest>(
                request,
                response_stream_writer.Object,
                call_number,
                propertyInfo);

            response_stream_writer.Setup(_ => _.WriteAsync(Moq.It.IsAny<MyResponse>())).Callback((MyResponse _) => response = _);
        };

        Because of = () => reverse_call.Reply(new MyResponse());

        It should_set_the_call_number = () => response.CallNumber.ShouldEqual(call_number);
    }
}