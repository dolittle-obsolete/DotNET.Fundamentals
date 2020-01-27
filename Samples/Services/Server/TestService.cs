// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Contracts;
using Dolittle.Logging;
using Grpc.Core;
using static Contracts.TestService;

namespace Server
{
    public class TestService : TestServiceBase
    {
        readonly ILogger _logger;

        public TestService(ILogger logger)
        {
            _logger = logger;
        }

        public override Task<Response> SayHelloTo(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Response { Message = $"Hello {request.Name}" });
        }

        public override async Task SayHelloToStream(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            await Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken).ConfigureAwait(false))
                {
                    _logger.Information($"Message received : '{requestStream.Current.Name}'");
                    await responseStream.WriteAsync(new Response { Message = $"Hello {requestStream.Current.Name}" }).ConfigureAwait(false);
                }
            }).ConfigureAwait(false);

            while (!context.CancellationToken.IsCancellationRequested)
            {
            }
        }
    }
}