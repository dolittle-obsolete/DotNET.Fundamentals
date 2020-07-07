// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Dolittle.Booting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static Contracts.TestService;

namespace Client
{
    static class Program
    {
        static async Task Main()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureLogging(_ => _.AddConsole());
            hostBuilder.UseEnvironment("Development");

            var host = hostBuilder.Build();
            var loggerFactory = host.Services.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            var result = Bootloader.Configure(_ =>
            {
                _.UseLoggerFactory(loggerFactory);
                _.Development();
            }).Start();
            var logger = result.Container.Get<Dolittle.Logging.ILogger>();

            var client = result.Container.Get<TestServiceClient>();
            var response = await client.SayHelloToAsync(new Request { Name = "Yoda" });
            logger.Information($"Response was : {response.Message}");

            var counter = 0;
            var stream = client.SayHelloToStream()
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (s, e) => stream.RequestStream.WriteAsync(new Request { Name = $"R2-D{counter++}" });
            timer.Start();

            var task = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext(CancellationToken.None).ConfigureAwait(false))
                {
                    logger.Information($"Response : {stream.ResponseStream.Current.Message}");
                }
            });

            await host.RunAsync().ConfigureAwait(false);

            timer.Dispose();

            loggerFactory.Dispose();
        }
    }
}
