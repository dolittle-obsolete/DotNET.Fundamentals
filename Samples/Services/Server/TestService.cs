// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Contracts;
using Grpc.Core;
using static Contracts.TestService;

namespace Server
{
    public class TestService : TestServiceBase
    {
        public override Task<Response> SayHelloTo(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Response { Message = $"Hello {request.Name}" });
        }
    }
}